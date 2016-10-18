using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using SomeProducts.CrossCutting.ProjectLogger;

namespace SomeProducts
{
    public class MvcApplication : HttpApplication
    {
        public static IContainer Container;
        protected void Application_Start()
        {
            Container = ContainerConfig.CreateContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exc = Server.GetLastError();
            ProjectLogger.Error($"Message: {exc.Message}; Source: {exc.Source}");
        }
    }
}
