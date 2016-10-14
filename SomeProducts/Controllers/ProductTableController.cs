using System.Web.Mvc;
using SomeProducts.Attribute;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;
using FilterInfo = SomeProducts.CrossCutting.Filter.Model.FilterInfo;

namespace SomeProducts.Controllers
{
    public class ProductTableController : Controller
    {
        private readonly IProductTablePresentationService _service;

        public ProductTableController(IProductTablePresentationService service)
        {
            _service = service;
        }

        [HttpGet]
        [AuthorizeRole(UserRole.Admin, UserRole.User)]
        public ActionResult Show(int? page, int? count, string by, 
            [ModelBinder(typeof(FilterInfoModelBinder))]FilterInfo filter)
        {
            var model = _service.GetTablePage(GetPageInfo(page, count, by), filter);
            return View("ProductTable", model);
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
    }
}