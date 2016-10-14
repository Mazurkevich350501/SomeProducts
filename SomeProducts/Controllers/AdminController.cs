
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

        public ActionResult Users(
            int? page,
            int? count,
            string by,
            [ModelBinder(typeof(FilterInfoModelBinder))]FilterInfo filter)
        {
            var pageInfo = new PageInfo(page, count, by);
            return View(_service.GetUserTableViewModel(pageInfo, filter));
        }

        [HttpPost]
        public async Task<JsonResult> ChangeUserAdminRole(int userId)
        {
            await _service.ChangeAdminRole(userId);
            return Json(await _service.GetUserRoles(userId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveUser(int userId, string redirectUrl)
        {
            await _service.RemoveUser(userId);
            return Redirect(redirectUrl);
        }

        [HttpPost]
        public JsonResult GetUserRoles(int userId)
        {
            var roles = _service.GetUserRoles(userId);
            return Json(roles);
        }
    }
}