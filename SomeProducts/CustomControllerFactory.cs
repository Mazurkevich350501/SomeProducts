﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;

namespace SomeProducts
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType != null
                ? (IController) MvcApplication.Container.Resolve(controllerType)
                : null;
        }
    }
}