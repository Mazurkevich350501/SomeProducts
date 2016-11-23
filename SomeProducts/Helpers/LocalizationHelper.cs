using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Routing;

namespace SomeProducts.Helpers
{
    public static class LocalizationHelper
    {
        public static void Localize(HttpContext context)
        {
            if (context == null)
                return;
            var culture = GetUrlCultureValue(context)
                           ?? GetCookiesCultureValue(context)
                           ?? "en";
            SetCulture(context, culture);
        }

        private static string GetUrlCultureValue(HttpContext context)
        {
            var requestContext = context?.Request.RequestContext?.HttpContext;
            if (requestContext == null)
                return null;
            var routeData = RouteTable.Routes.GetRouteData(requestContext);
            return routeData?.Values["culture"] as string;
        }

        private static string GetCookiesCultureValue(HttpContext context)
        {
            var request = context?.Request;
            return  request?.Cookies["culture"]?.Value;
        }

        private static void SetCulture(HttpContext context, string culture)
        {
            var ci = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            var cookie = new HttpCookie("culture", ci.Name);
            var request = context.Request;
            request.Cookies.Add(cookie);
        }
    }
}