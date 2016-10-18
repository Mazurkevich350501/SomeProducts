using System;
using System.Globalization;
using System.Threading;
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

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null) return;

            var ci = (CultureInfo) Session["Culture"];
            if (ci == null)
            {
                var langName = "en";
                if (HttpContext.Current.Request.UserLanguages != null &&
                    HttpContext.Current.Request.UserLanguages.Length != 0)
                {
                    langName = HttpContext.Current.Request.UserLanguages[0];
                }
                ci = new CultureInfo(langName);
                Session["Culture"] = ci;
            }
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }
    }
}
