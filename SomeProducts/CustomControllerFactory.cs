using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using SomeProducts.CrossCutting.ProjectLogger;
using SomeProducts.Helpers;

namespace SomeProducts
{
    [HandleErrorLog]
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            LocalizationHelper.Localize(HttpContext.Current);
            return controllerType != null
                ? (IController)AutofacDependencyResolver.Current.RequestLifetimeScope.Resolve(controllerType)
                : null;
        }
    }
}