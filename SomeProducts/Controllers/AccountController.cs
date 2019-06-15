
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Resources;
using SomeProducts.CrossCutting.ProjectLogger;
using SomeProducts.Helpers;
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
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationViewModel model, string returnUrl)
        {
            ProjectLogger.Trace($"Register new user {model.Name}");
            if (ModelState.IsValid)
            {
                var result = await _manager.PasswordValidator.ValidateAsync(model.Password);
                if (result.Succeeded)
                {
                    
                    result = await _manager.CreateAsync(AccountManager.UserCast(model));
                    if (result.Succeeded)
                    {
                        await LogIn(model.ToLogInUserModel());
                        return Redirect(returnUrl);
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(LogInUserModel model, string returnUrl)
        {
            ProjectLogger.Trace($"User {model.Name} try to login");
            if (!ModelState.IsValid) return View(model);
            var result = await LogIn(model);
            if (result.Succeeded)
            {
                if (returnUrl == null)
                {
                    ProjectLogger.Trace($"Login User {model.Name}");
                    return RedirectToAction("Show", "ProductTable");
                }
                return Redirect(returnUrl);
            }

            ModelState.AddModelError("Error", result.Errors.First());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff()
        {
            ProjectLogger.Trace($"LogOff user {HttpContext.User.Identity.Name}");
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("LogIn", "Account");
        }

        [HttpGet]
        public ActionResult ChangeCulture(string returnUrl)
        {
            LocalizationHelper.Localize(System.Web.HttpContext.Current);
            return Redirect(returnUrl);
        }
        
        private async Task<IdentityResult> LogIn(LogInUserModel userModel)
        {
            var user = await _manager.FindAsync(userModel.Name, userModel.Password);
            if (user == null)
            {
                var error = await _manager.FindByNameAsync(userModel.Name) == null
                    ? LocalResource.IncorrectUserName
                    : LocalResource.IncorrectPassword;
                return IdentityResult.Failed(error);
            }

            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await user.GenerateUserIdentityAsync(_manager);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = false
            };
            HttpContext.GetOwinContext().Authentication.SignIn(properties, identity);
            return IdentityResult.Success;
        }
    }
}