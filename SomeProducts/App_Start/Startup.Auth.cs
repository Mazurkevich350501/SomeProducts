using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.Authorize;

namespace SomeProducts
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                     
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AccountManager, User, int>(
                        validateInterval: TimeSpan.FromSeconds(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (user) => (int.Parse(user.GetUserId())))
                }
            }); 
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}

