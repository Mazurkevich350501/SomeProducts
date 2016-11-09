
using System.Web.Mvc;
using SomeProducts.Attribute;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.PresentationServices.IPresentationSevices.Audit;
using SomeProducts.PresentationServices.Models;
using FilterInfo = SomeProducts.CrossCutting.Filter.Model.FilterInfo;

namespace SomeProducts.Controllers
{
    [AuthorizeRole(UserRole.Admin, UserRole.SuperAdmin)]
    public class AuditController : Controller
    {
        private readonly IAuditPresentationService _service;

        public AuditController(IAuditPresentationService service)
        {
            _service = service;
        }

        public ActionResult FullAudit(
            int? page,
            int? count,
            string by,
            [ModelBinder(typeof(FilterInfoModelBinder))]FilterInfo filter)
        {
            var pageInfo = new PageInfo(page, count, by);
            int? companyId = null;
            if(!User.IsInRole(nameof(UserRole.SuperAdmin))) companyId = User.GetCompany();
            var model = _service.GetFullAuditViewTable(pageInfo, filter, companyId);
            return View("AuditTable", model);
        }
    }
}