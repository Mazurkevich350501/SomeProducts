using System.Web.Mvc;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SomeProducts.Startup))]
namespace SomeProducts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ControllerBuilder.Current.SetControllerFactory(typeof(CustomControllerFactory));
            ConfigureAuth(app);
        }
    }
}
