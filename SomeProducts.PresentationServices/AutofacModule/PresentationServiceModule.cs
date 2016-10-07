

using Autofac;
using Microsoft.Owin;
using SomeProducts.PresentationServices.Authorize;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.PresentaoinServices;
using SomeProducts.PresentationServices.PresentaoinServices.Create;
using SomeProducts.PresentationServices.PresentaoinServices.ProductTable;

namespace SomeProducts.PresentationServices.AutofacModule
{
    public class PresentationServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OwinContext>().As<IOwinContext>();
            builder.RegisterType<AccountManager>().As<AccountManager>();
            builder.RegisterType<UserPresentationService>().As<UserPresentationService>();
            builder.RegisterType<ProductViewModelPresentationService>().As<IProductViewModelPresentationService>();
            builder.RegisterType<BrandModelPresentationService>().As<IBrandModelPresentationService>();
            builder.RegisterType<ProductTablePresentationService>().As<IProductTablePresentationService>();
        }
    }
}
