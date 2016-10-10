using System;
using System.Linq;
using System.Linq.Expressions;
using SomeProducts.CrossCutting.Filter.Model;

namespace SomeProducts.CrossCutting.Filter
{
    public static class DataFiltration
    {
        public static IQueryable<T> GetFilteredProuct<T>(this IQueryable<T> query, FilterInfo filters)
        {
            var a = filters?.Filters != null
                ? filters.Filters.Aggregate(query, (current, filter) => current.Where(filter))
                : query;
            return a;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Model.Filter filterParam)
        {
            if (filterParam == null)
                return query;

            var parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;
            foreach (var property in filterParam.Option.Split('_'))
                memberAccess = Expression.Property
                   (memberAccess ?? ((Expression)parameter), property);

            var filter = Expression.Constant(memberAccess != null && memberAccess.Type.BaseType != typeof(string)
                ? Convert.ChangeType(filterParam.Value, memberAccess.Type) : filterParam.Value);

            if (memberAccess == null) return query;
            Expression condition;
            LambdaExpression lambda;
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
                    condition = Expression.Equal(memberAccess, Expression.Constant(null));
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsNotNull:
                    condition = Expression.NotEqual(memberAccess, Expression.Constant(null));
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.DoesNotContain:
                    condition = Expression.Not(Expression.Call(memberAccess,
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                        Expression.Constant(filterParam.Value)));
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsEmty:
                    condition = Expression.Equal(memberAccess, Expression.Constant(""));
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                case FilterParameter.IsNotEmty:
                    condition = Expression.NotEqual(memberAccess, Expression.Constant(""));
                    lambda = Expression.Lambda(condition, parameter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterParam.Option), filterParam.Option, null);
            }

            var result = Expression.Call(typeof(Queryable), "Where", new[] { query.ElementType }, query.Expression, lambda);

            return query.Provider.CreateQuery<T>(result);
        }
    }
}