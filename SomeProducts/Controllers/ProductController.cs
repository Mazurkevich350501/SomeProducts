
using System.Linq;
using System.Web.Mvc;
using SomeProducts.Attribute;
using SomeProducts.CrossCutting.Utils;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ProductController : Controller
    {
        private readonly IProductViewModelPresentationService _productViewModelService;
        private readonly IBrandModelPresentationService _barndModelService;

        public ProductController(IProductViewModelPresentationService productViewModelService, IBrandModelPresentationService barndModelService)
        {
            _productViewModelService = productViewModelService;
            _barndModelService = barndModelService;
        }
        
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
            if (productModel.Product == null)
            {
                return View("Error", (object)"Product was not found");
            }
            return View("Create", productModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ImageUtils.AddImageToModel(model.Product, Request);
                _productViewModelService.CreateProductViewModel(model);
                var productModel = _productViewModelService.GetLastProductViewMode();
                return RedirectToAction("Edit", "Product", new { id = productModel.Product.Id });
            }
            var newModel = _productViewModelService.GetProductViewModel();
            newModel.Product = model.Product;
            return View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ImageUtils.AddImageToModel(model.Product, Request);
                var result = _productViewModelService.UpdateProductViewModel(model);
                if (result)
                {
                    return RedirectToAction("Edit", "Product", new { id = model.Product.Id });
                }
                return View("Error", (object)"Product already has changed");
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
        [AuthorizeRole(UserRole.Admin)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int productId, string redirectUrl)
        {
            _productViewModelService.RemoveProductViewModel(productId);
            return Redirect(redirectUrl);
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