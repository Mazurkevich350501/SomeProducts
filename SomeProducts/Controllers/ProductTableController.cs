
using System.Web.Mvc;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.CrossCutting.ProjectLogger;
using SomeProducts.Helpers;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models;
using FilterInfo = SomeProducts.CrossCutting.Filter.Model.FilterInfo;

namespace SomeProducts.Controllers
{
    [HandleErrorLog]
    [Authorize]
    public class ProductTableController : Controller
    {
        private readonly IProductTablePresentationService _service;

        public ProductTableController(IProductTablePresentationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Show(
            int? page,
            int? count,
            string by, 
            [ModelBinder(typeof(FilterInfoModelBinder))]FilterInfo filter)
        {
            var pageInfo = new PageInfo(page, count, by);
            int? companyId = null;
            if (!User.IsInRole(nameof(UserRole.SuperAdmin)))
            {
                companyId = User.GetCompany();
            }
            var model = _service.GetTablePage(pageInfo, filter, companyId);
            return View("ProductTable", model);
        }
    }
}