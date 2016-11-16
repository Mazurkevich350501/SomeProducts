using Autofac;
using Autofac.Integration.Mvc;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.Helpers;

namespace SomeProducts.AutofacModule
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<UserHelper>().As<IUserHelper>();
        }
    }
}