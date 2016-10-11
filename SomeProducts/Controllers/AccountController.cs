
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SomeProducts.PresentationServices.Models.Account;
using SomeProducts.PresentationServices.PresentationServices;

namespace SomeProducts.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserPresentationService _userManager;

        public AccountController(UserPresentationService userManager)
        {
            _userManager = userManager;
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
                var result = await _userManager.PasswordValidateAsync(model.Password);
                if (result.Succeeded)
                {
                    result = await _userManager.CreateAsync(model);
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

            if (await _userManager.LogIn(model, HttpContext.GetOwinContext().Authentication))
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

    }
}