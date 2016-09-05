
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SomeProducts.PresentationServices.Dao;
using SomeProducts.PresentationServices.IDao;
using SomeProducts.PresentationServices.Models;


namespace SomeProducts.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductViewModelDao _productViewModelService = new ProductViewModelDao();
        private readonly IBrandModelDao _barndModelService = new BrandModelDao();

        // GET: Product
        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel model = _productViewModelService.GetProductViewModel(null);
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productModel = _productViewModelService.GetProductViewModel(id);
            if(productModel == null)
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
                SaveImage(model, Request);
                _productViewModelService.CreateProductViewModel(model);
                var productModel = _productViewModelService.GetLastProductViewMode();
                return Redirect(Url.Action("Edit", "Product", new {id = productModel.Product.ProductId }));
            }
            var newModel = _productViewModelService.GetProductViewModel(null);
            newModel.Product = model.Product;
            return View(newModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                SaveImage(model, Request);
                _productViewModelService.UpdateProductViewModel(model);
            }
            var newModel = _productViewModelService.GetProductViewModel(null);
            newModel.Product = model.Product;
            return View("Create",newModel);
        }

        public JsonResult SaveBrandsChanges(BrandsChangeModel changeModel)
        {
            if(changeModel != null)
            {
                if(changeModel.RemovedBrands != null)
                {
                    foreach (var brand in changeModel.RemovedBrands)
                    {
                        _barndModelService.RemoveBrand(brand.BrandId);
                    }
                }
                if(changeModel.AddedBrands != null)
                {
                    foreach (var brand in changeModel.AddedBrands)
                    {
                        _barndModelService.CreateBrand(brand);
                    }
                }
            }
            return GetBrandsList();
        }

        [HttpPost]
        public JsonResult Delete(int productId)
        {
           _productViewModelService.RemoveProductViewModel(productId);
            return Json(Url.Action("Create", "Product"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBrandsList()
        {
            return Json(_barndModelService.GetAllItems().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBrandUsing(int id)
        {
            return Json(_barndModelService.IsBrandModelUsing(id), JsonRequestBehavior.AllowGet);
        }

        private void SaveImage(ProductViewModel model, HttpRequestBase request)
        {
            if (Request.Files.Count > 0)
            {
                var image = request.Files[0];
                if (image != null && image.ContentLength > 0)
                {
                    model.Product.Image = new byte[image.ContentLength];
                    image.InputStream.Read(model.Product.Image, 0, image.ContentLength);
                    model.Product.ImageType = image.ContentType;
                }
            }     
        }
    }
}