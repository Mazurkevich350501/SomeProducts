using Autofac;
using SomeProducts.Controllers;

namespace SomeProducts.AutofacModule
{
    public class ControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductController>().As<ProductController>();
            builder.RegisterType<AccountController>().As<AccountController>();
            builder.RegisterType<AdminController>().As<AdminController>();
            builder.RegisterType<ProductTableController>().As<ProductTableController>();
            builder.RegisterType<ErrorController>().As<ErrorController>();
            builder.RegisterType<AuditController>().As<AuditController>();
        }
    }
}