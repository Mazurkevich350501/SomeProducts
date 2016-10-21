
using System.Threading.Tasks;
using System.Web.Mvc;
using SomeProducts.Attribute;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.CrossCutting.ProjectLogger;
using SomeProducts.PresentationServices.IPresentationSevices.Admin;
using SomeProducts.PresentationServices.Models;
using FilterInfo = SomeProducts.CrossCutting.Filter.Model.FilterInfo;

namespace SomeProducts.Controllers
{
    [AuthorizeRole(UserRole.Admin)]
    [HandleErrorLog]
    public class AdminController : Controller
    {
        private readonly IUserTablePresentationService _service;

        public AdminController(IUserTablePresentationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Users(
            int? page,
            int? count,
            string by,
            [ModelBinder(typeof(FilterInfoModelBinder))]FilterInfo filter)
        {
            ProjectLogger.Trace($"User {HttpContext.User.Identity.Name} open admin page");
            var pageInfo = new PageInfo(page, count, by);
            return View(_service.GetUserTableViewModel(pageInfo, filter));
        }

        [HttpPost]
        public async Task<JsonResult> ChangeUserAdminRole(int userId)
        {
            ProjectLogger.Trace($"User {HttpContext.User.Identity.Name} try change role for user(id={userId})");
            if (!IsActiveUser(userId))
            {
                await _service.ChangeAdminRole(userId);
            }
            return Json(await _service.GetUserRoles(userId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveUser(int userId, string redirectUrl)
        {
            ProjectLogger.Trace($"User {HttpContext.User.Identity.Name} try remove user(id={userId})");
            if (!IsActiveUser(userId))
            {
                await _service.RemoveUser(userId);
            }
            return Redirect(redirectUrl);
        }

        [HttpPost]
        public JsonResult GetUserRoles(int userId)
        {
            var roles = _service.GetUserRoles(userId);
            return Json(roles);
        }

        private bool IsActiveUser(int userId)
        {
            var userName = HttpContext.User.Identity.Name;
            return _service.IsUserExist(userId, userName);
        }
    }
}