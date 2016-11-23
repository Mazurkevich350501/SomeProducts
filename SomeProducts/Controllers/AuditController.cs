
using System.Web.Mvc;
using SomeProducts.Attribute;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.IPresentationSevices.Audit;
using SomeProducts.PresentationServices.Models;
using FilterInfo = SomeProducts.CrossCutting.Filter.Model.FilterInfo;

namespace SomeProducts.Controllers
{
    [AuthorizeRole(UserRole.Admin, UserRole.SuperAdmin)]
    [RequireHttps]
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
            var model = _service.GetFullAuditViewTable(pageInfo, filter);
            return View("AuditTable", model);
        }

        public ActionResult ItemAudit(
            int id,
            string entity,
            int? page,
            int? count,
            string by)
        {
            var pageInfo = new PageInfo(page, count, by);
            var model = _service.GetAuditViewTableForItem(pageInfo, entity, id);
            return View("AuditTableForItem", model);
        }
    }
}