
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
    }
}
