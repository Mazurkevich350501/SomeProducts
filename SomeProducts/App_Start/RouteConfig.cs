﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SomeProducts
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PageRoute",
                url: "{controller}/{action}/Page{page}",
                defaults: new { controller = "ProductTable", action = "Show", page = 1 }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "Create", id = UrlParameter.Optional }
            );
        }
    }
}
