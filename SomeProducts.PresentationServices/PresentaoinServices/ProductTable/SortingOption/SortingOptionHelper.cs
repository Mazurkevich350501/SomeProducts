using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SomeProducts.DAL.Models;

namespace SomeProducts.PresentationServices.PresentaoinServices.ProductTable.SortingOption
{
    public static class SortingOptionHelper
    {
        private static readonly Dictionary<string, string> SortingOptionDictionary;

        static SortingOptionHelper()
        {
            SortingOptionDictionary = new Dictionary<string, string>
            {
                {"Brand", $"{nameof(Product.Brand)}.{nameof(Brand.Name)}"},
                {"Name", nameof(Product.Name)},
                {"Quantity", nameof(Product.Quantity)},
            };
        }

        public static SortingOption GetOptionValue(string key)
        {
            Order order;
            if (key.Substring(0, 3) == "rev")
            {
                order = Order.Reverse;
                key = key.Remove(0, 3);
            }
            else
            {
                order = Order.Original;
            }
            var option = SortingOptionDictionary.Keys.Any(k => k == key)
                ? SortingOptionDictionary[key]
                : nameof(Product.Name);

            return new SortingOption(order, option);
        }

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
    }
}
