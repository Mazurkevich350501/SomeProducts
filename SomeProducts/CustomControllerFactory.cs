using System;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using SomeProducts.Controllers;

namespace SomeProducts
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {      
            return (IController)MvcApplication.Container.Resolve<ProductController>();
        }
    }
}