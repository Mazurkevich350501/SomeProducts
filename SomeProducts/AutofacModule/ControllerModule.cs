using Autofac;
using SomeProducts.Controllers;

namespace SomeProducts.AutofacModule
{
    public class ControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductController>().As<ProductController>();
            builder.RegisterType<ProductTableController>().As<ProductTableController>();
        }
    }
}