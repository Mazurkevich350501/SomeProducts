using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SomeProducts.PresentationServices.Models.Account;
using SomeProducts.PresentationServices.PresentaoinServices;

namespace SomeProducts.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserPresentationService _userManager;

        public AccountController(UserPresentationService userManager)
        {
            _userManager = userManager;
        }
        
        public ActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
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
                        return RedirectToAction("Create", "Product");
                    }
                }
                ModelState.AddModelError("Error", result.Errors.First());
            }
            return View(new RegistrationViewModel());
        }

        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LogInUserModel());
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(LogInUserModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _userManager.LogIn(model, HttpContext.GetOwinContext().Authentication))
            {
                return Redirect(returnUrl);
            }

            ModelState.AddModelError("Error", "Invalid username or password.");
            return View(model);
        }
    }
}