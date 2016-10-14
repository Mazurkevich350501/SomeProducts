
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SomeProducts.CrossCutting.Sorting.SortingOption
{
    public static class SortingOptionHelper
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string option, bool direction)
        {
            string methodName = $"OrderBy{(!direction ? "" : "descending")}";
            var parameter = Expression.Parameter(query.ElementType, "p");
            MemberExpression memberAccess = null;
            foreach (var property in option.Split('.'))
                memberAccess = Expression.Property
                   (memberAccess ?? ((Expression)parameter), property);

            if (memberAccess == null) return query;
            var orderByLambda = Expression.Lambda(memberAccess, parameter);
            var result = Expression.Call(
                      typeof(Queryable),
                      methodName,
                      new[] { query.ElementType, memberAccess.Type },
                      query.Expression,
                      Expression.Quote(orderByLambda));

            return query.Provider.CreateQuery<T>(result);
        }

        public static SortingOption GetOptionValue(string key, Dictionary<string, string> optionDictionary)
        {
            key = key ?? "";
            Order order;
            if (key.Length > 3 && key.Substring(0, 3) == "rev")
            {
                order = Order.Reverse;
                key = key.Remove(0, 3);
            }
            else
            {
                order = Order.Original;
            }
            var option = optionDictionary.Keys.Any(k => k == key)
                ? optionDictionary[key]
                : optionDictionary.Values.First();

            return new SortingOption(order, option);
        }
    }
}
