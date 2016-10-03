using System.Threading.Tasks;
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
                var result = await _userManager.CreateAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Create", "Product");
                    //return RedirectToAction("Login", "Account");
                }
            }
            return View(new RegistrationViewModel());
            //return System.Web.UI.WebControls.View(model);
        }
    }
}