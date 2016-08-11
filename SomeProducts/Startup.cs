using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SomeProducts.Startup))]
namespace SomeProducts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
