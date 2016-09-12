
using System.Linq;
using System.Web.Mvc;
using SomeProducts.PresentationServices.IPresentationSevices;
using SomeProducts.PresentationServices.Models;
using SomeProducts.CrossCutting.Utils;

namespace SomeProducts.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductViewModelPresentationService _productViewModelService;
        private readonly IBrandModelPresentationService _barndModelService;

        public ProductController(IProductViewModelPresentationService productViewModelService, IBrandModelPresentationService barndModelService)
        {
            _productViewModelService = productViewModelService;
            _barndModelService = barndModelService;
        }

        // GET: Product
        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel model = _productViewModelService.GetProductViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productModel = _productViewModelService.GetProductViewModel(id);
            if (productModel == null)
            {
                return View("Error");
            }
            return View("Create", productModel);
        }

        // POST: /Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ImageUtils.AddImageToModel(model.Product, Request);
                _productViewModelService.CreateProductViewModel(model);
                var productModel = _productViewModelService.GetLastProductViewMode();
                return RedirectToAction("Edit", "Product", new { id = productModel.Product.ProductId });
            }
            var newModel = _productViewModelService.GetProductViewModel();
            newModel.Product = model.Product;
            return View(newModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ImageUtils.AddImageToModel(model.Product, Request);
                _productViewModelService.UpdateProductViewModel(model);
            }
            var newModel = _productViewModelService.GetProductViewModel();
            newModel.Product = model.Product;
            return View("Create", newModel);
        }

        public JsonResult SaveBrandsChanges(BrandsChangeModel changeModel)
        {
            _barndModelService.SaveBrandChanges(changeModel);
            return GetBrandsList();
        }

        [HttpPost]
        public JsonResult Delete(int productId)
        {
            _productViewModelService.RemoveProductViewModel(productId);
            var a = Url.Action("Create", "Product");
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBrandsList()
        {
            return Json(_barndModelService.GetAllItems().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBrandUsing(int id)
        {
            return Json(_barndModelService.IsBrandModelUsing(id), JsonRequestBehavior.AllowGet);
        }
    }
}