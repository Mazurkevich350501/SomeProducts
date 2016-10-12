
using System.Threading.Tasks;
using System.Web.Mvc;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.IPresentationSevices.Admin;
using SomeProducts.PresentationServices.Models;
using FilterInfo = SomeProducts.CrossCutting.Filter.Model.FilterInfo;

namespace SomeProducts.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserTablePresentationService _service;

        public AdminController(IUserTablePresentationService service)
        {
            _service = service;
        }

        public ActionResult Users(int? page, int? count, string by,
            [ModelBinder(typeof(FilterInfoModelBinder))]FilterInfo filter)
        {
            return View(_service.GetUserTableViewModel(GetPageInfo(page, count, by), filter));
        }

        private static PageInfo GetPageInfo(int? page, int? count, string by)
        {
            return new PageInfo()
            {
                Page = page ?? 1,
                ProductCount = count ?? 5,
                SortingOption = by ?? "Name"
            };
        }

        [HttpPost]
        public async Task<JsonResult> ChangeUserAdminRole(int userId)
        {
            await _service.ChangeAdminRole(userId);
            return Json(await _service.IsUserAdmin(userId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveUser(int userId, string redirectUrl)
        {
            await _service.RemoveUser(userId);
            return Redirect(redirectUrl);
        }
    }
}