using Autofac;
using SomeProducts.Controllers;
using SomeProducts.PresentationServices.IPresentationSevices;
using SomeProducts.PresentationServices.PresentaoinServices;

namespace SomeProducts
{
    public static class ContainerConfig
    { 
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ProductViewModelPresentationService>().As<IProductViewModelPresentationService>().SingleInstance();
            builder.RegisterType<BrandModelPresentationService>().As<IBrandModelPresentationService>().SingleInstance();
            builder.RegisterType<ProductController>().As<ProductController>();
            return builder.Build();
        }
    }
}