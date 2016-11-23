
using System.Web.Mvc;
using System.Web.Routing;

namespace SomeProducts
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "DefaultCulture",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional },
                constraints: new { culture = "[a-z]{2}" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { culture = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "PageRoute",
               url: "{controller}/{action}",
               defaults: new { controller = "ProductTable", action = "Show"}
            );
        }
    }
}
