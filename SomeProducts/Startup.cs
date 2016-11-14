using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SomeProducts.Startup))]
namespace SomeProducts
{
    public partial class Startup
    {
        public static IContainer Container;

        public void Configuration(IAppBuilder app)
        {
            Container = ContainerConfig.CreateContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(typeof(CustomControllerFactory));
            ConfigureAuth(app);
        }
    }
}
