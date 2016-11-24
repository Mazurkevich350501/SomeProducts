
using Autofac;
using SomeProducts.PresentationServices.Authorize;
using SomeProducts.PresentationServices.IPresentationSevices.Admin;
using SomeProducts.PresentationServices.IPresentationSevices.Audit;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.PresentationServices.Admin;
using SomeProducts.PresentationServices.PresentationServices.Audit;
using SomeProducts.PresentationServices.PresentationServices.Create;
using SomeProducts.PresentationServices.PresentationServices.ProductTable;

namespace SomeProducts.PresentationServices.AutofacModule
{
    public class PresentationServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountManager>().As<AccountManager>();
            builder.RegisterType<UserTablePresentationService>().As<IUserTablePresentationService>();
            builder.RegisterType<ProductViewModelPresentationService>().As<IProductViewModelPresentationService>();
            builder.RegisterType<BrandModelPresentationService>().As<IBrandModelPresentationService>();
            builder.RegisterType<ProductTablePresentationService>().As<IProductTablePresentationService>();
            builder.RegisterType<AuditPresentationService>().As<IAuditPresentationService>();
            builder.RegisterType<CompanyPresentationService>().As<ICompanyPresentationService>();
        }
    }
}
