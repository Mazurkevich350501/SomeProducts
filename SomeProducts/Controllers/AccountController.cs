
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SomeProducts.PresentationServices.Authorize;
using SomeProducts.PresentationServices.Models.Account;

namespace SomeProducts.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountManager _manager;

        public AccountController(AccountManager userManager)
        {
            _manager = userManager;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _manager.PasswordValidator.ValidateAsync(model.Password);
                if (result.Succeeded)
                {
                    result = await _manager.CreateAsync(AccountManager.UserCast(model));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("LogIn", "Account");
                    }
                }
                ModelState.AddModelError("Error", result.Errors.First());
            }
            return View(new RegistrationViewModel());
        }

        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LogInUserModel());
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LogIn(LogInUserModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            if (await LogIn(model))
            {
                return RedirectToAction("Show", "ProductTable");
            }

            ModelState.AddModelError("Error", "Invalid username or password.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("LogIn", "Account");
        }

        private async Task<bool> LogIn(LogInUserModel userModel)
        {
            var user = await _manager.FindAsync(userModel.Name, userModel.Password);
            if (user == null) return false;

            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.GetOwinContext()
                .Authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            return true;
        }
    }
}