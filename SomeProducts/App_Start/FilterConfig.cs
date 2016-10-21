

using System.Web.Mvc;
using SomeProducts.CrossCutting.ProjectLogger;
using SomeProducts.ProjectGlobalFilters;

namespace SomeProducts
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorLogAttribute());
            filters.Add(new LocalizationFilter());
        }
    }
}
