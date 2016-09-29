using System.Web.Mvc;
using System.Web.Script.Serialization;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;
using FilterInfo = SomeProducts.CrossCutting.Filter.FilterInfo;

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
        public ActionResult Show(int? page, int? count, string by, string filterJson)
        {
            var filterInfo = filterJson != null 
                ? new JavaScriptSerializer().Deserialize<FilterInfo>(filterJson) 
                : null;
            var model = _service.GetTablePage(GetPageInfo(page, count, by), filterInfo);
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