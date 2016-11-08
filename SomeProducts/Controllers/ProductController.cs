
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SomeProducts.Attribute;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.CrossCutting.ProjectLogger;
using SomeProducts.CrossCutting.Utils;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.Controllers
{
    [Authorize]
    [HandleErrorLog]
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
        [AuthorizeRole(UserRole.Admin)]
        public ActionResult Create()
        {
            var model = _productViewModelService.GetEmtyProductViewModel(User.GetCompany());
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productModel = _productViewModelService.GetProductViewModel(User.GetCompany(), id);
            if (productModel?.Product == null)
            {
                throw new HttpException(404, "Wrong product id");
            }
            return HttpContext.User.IsInRole(nameof(UserRole.Admin)) 
                ? View("Create", productModel)
                : View("ShowProduct", productModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.Admin)]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ImageUtils.AddImageToModel(model.Product, Request);
                _productViewModelService.CreateProductViewModel(model, User.GetCompany(), User.GetUserId());
                var productModel = _productViewModelService.GetLastProductViewMode(User.GetCompany());

                ProjectLogger.Trace($"User {HttpContext.User.Identity.Name} create new product(id={productModel.Product.Id})");
                return RedirectToAction("Edit", "Product", new { id = productModel.Product.Id });
            }
            var newModel = _productViewModelService.GetEmtyProductViewModel(User.GetCompany());
            newModel.Product = model.Product;
            return View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.Admin)]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ImageUtils.AddImageToModel(model.Product, Request);
                var result = _productViewModelService.UpdateProductViewModel(model, User.GetCompany(), User.GetUserId());
                if (result)
                {
                    ProjectLogger.Trace($"User {HttpContext.User.Identity.Name} edit product(id={model.Product.Id})");
                    return RedirectToAction("Edit", "Product", new { id = model.Product.Id });
                }
                return View("Error", (object)"Product already has changed");
            }

            var newModel = _productViewModelService.GetEmtyProductViewModel(User.GetCompany());
            newModel.Product = model.Product;
            return View("Create", newModel);
        }

        [AuthorizeRole(UserRole.Admin)]
        public JsonResult SaveBrandsChanges(BrandsChangeModel changeModel)
        {
            ProjectLogger.Trace($"User {HttpContext.User.Identity.Name} change brands ({changeModel})");
            _barndModelService.SaveBrandChanges(changeModel, User.GetCompany(), User.GetUserId());
            return GetBrandsList();
        }

        [HttpPost]
        [AuthorizeRole(UserRole.Admin)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int productId, string redirectUrl)
        {
            ProjectLogger.Trace($"User {HttpContext.User.Identity.Name} remove product (id={productId})");
            _productViewModelService.RemoveProductViewModel(productId, User.GetUserId());
            return Redirect(redirectUrl);
        }

        public JsonResult GetBrandsList()
        {
            return Json(_barndModelService.GetCompanyBrands(User.GetCompany()).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBrandUsing(int id)
        {
            return Json(_barndModelService.IsBrandModelUsing(User.GetCompany(), id), JsonRequestBehavior.AllowGet);
        }
    }
}