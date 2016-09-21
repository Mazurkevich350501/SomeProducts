using System.Web.Mvc;
using PagedList;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;

namespace SomeProducts.Controllers
{
    public class ProductTableController : Controller
    {
        private readonly IProductTablePresentationService _service;

        public ProductTableController(IProductTablePresentationService service)
        {
            _service = service;
        }

        // GET: ProductTable
        public ActionResult Show(int? page, int? count, string by)
        {
            var pageInfo = new PageInfo()
            {
                Page = page ?? 1,
                ProductCount = count ?? 5,
                SortingOption = by ?? "Name"
            };
            var model = _service.GetTablePage(pageInfo);
            return View("ProductTable", model);
        }
    }
}