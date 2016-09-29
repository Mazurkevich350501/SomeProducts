﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace SomeProducts.CrossCutting.Filter
{
    public static class DataFiltration
    {
        public static IQueryable<T> GetFilteredProuct<T>(this IQueryable<T> query, FilterInfo filters)
        {
            return filters?.Filters != null
                ? filters.Filters.Aggregate(query, (current, filter) => current.Where(filter))
                : query;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter filterParam)
        {
            if (filterParam == null)
                return query;

            var parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;
            foreach (var property in filterParam.Option.Split('.'))
                memberAccess = Expression.Property
                   (memberAccess ?? ((Expression)parameter), property);

            var filter = Expression.Constant(memberAccess != null && memberAccess.Type.BaseType != typeof(string)
                ? Convert.ChangeType(filterParam.Value, memberAccess.Type) : filterParam.Value);

            Expression condition;
            LambdaExpression lambda = null;
            switch (filterParam.Parameter)
            {
                case FilterParameter.IsEqualTo:
                    condition = Expression.Equal(memberAccess, filter);
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsNotEqualTo:
                    condition = Expression.NotEqual(memberAccess, filter);
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsLessThenOrEqualTo:
                    condition = Expression.LessThanOrEqual(memberAccess, filter);
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsGreaterThanOrEqualTo:
                    condition = Expression.GreaterThanOrEqual(memberAccess, filter);
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.Contains:
                    condition = Expression.Call(memberAccess,
                        typeof(string).GetMethod("Contains"),
                        Expression.Constant(filterParam.Value));
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.EndsWith:
                    condition = Expression.Call(memberAccess,
                        typeof(string).GetMethod("EndsWith", new[] { typeof(string) }),
                        Expression.Constant(filterParam.Value));
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsLessThen:
                    condition = Expression.LessThan(memberAccess, filter);
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsNull:
                    break;
                case FilterParameter.IsNotNull:
                    break;
                case FilterParameter.DoesNotContain:
                    break;
                case FilterParameter.IsEmty:
                    condition = Expression.Call(memberAccess,
                        typeof(string).GetMethod("Equals", new[] { typeof(string) }),
                        Expression.Constant(""));
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsNotEmty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterParam.Option), filterParam.Option, null);
            }

            var result = Expression.Call(typeof(Queryable), "Where", new[] { query.ElementType }, query.Expression, lambda);

            return query.Provider.CreateQuery<T>(result);
        }
    }
}