using Autofac;
using SomeProducts.AutofacModule;
using SomeProducts.DAL.AutofacModule;
using SomeProducts.PresentationServices.AutofacModule;

namespace SomeProducts
{
    public static class ContainerConfig
    { 
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DalModule>();
            builder.RegisterModule<PresentationServiceModule>();
            builder.RegisterModule<ControllerModule>();
            return builder.Build();
        }
    }
}