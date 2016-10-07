

using Autofac;
using Microsoft.Owin;
using SomeProducts.PresentationServices.Authorize;
using SomeProducts.PresentationServices.IPresentationSevices;
using SomeProducts.PresentationServices.PresentaoinServices;

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
        }
    }
}
