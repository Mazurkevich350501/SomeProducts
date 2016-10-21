using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SomeProducts.ProjectGlobalFilters
{
    public class LocalizationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session == null) return;

            if (filterContext.HttpContext.Session != null)
            {
                var ci = (CultureInfo)filterContext.HttpContext.Session["Culture"];
                if (ci == null)
                {
                    var langName = "en";
                    if (HttpContext.Current.Request.UserLanguages != null &&
                        HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        langName = HttpContext.Current.Request.UserLanguages[0];
                    }
                    ci = new CultureInfo(langName);
                    filterContext.HttpContext.Session["Culture"] = ci;
                }
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }
    }
}