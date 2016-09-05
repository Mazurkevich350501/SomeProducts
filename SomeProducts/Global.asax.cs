using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;

namespace SomeProducts
{
    public class MvcApplication : System.Web.HttpApplication
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
    }
}
