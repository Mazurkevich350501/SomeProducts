
using System.Web.Mvc;
using SomeProducts.CrossCutting.ProjectLogger;

namespace SomeProducts
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorLogAttribute());
        }
    }
}
