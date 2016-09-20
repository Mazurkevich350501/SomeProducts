using System;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;

namespace SomeProducts
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return MvcApplication.Container.Resolve(controllerType) as IController;
        }
    }
}