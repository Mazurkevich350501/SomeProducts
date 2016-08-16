using SomeProducts.DbRepository;
using SomeProducts.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SomeProducts.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProductViewModel()
            {
                Product = new Product(),
                Brands = BrandDictionary(),
                Colors = new ProductColors().Colors
            };         
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = new ProductRepository().GetById(id);
            if(product == null)
            {
                throw new HttpException(404, "Are you sure you're in the right place?");
            }
            var model = new ProductViewModel()
            {
                Product = product,
                Brands = BrandDictionary(),
                Colors = new ProductColors().Colors
            };
            return View("Create", model);
        }

        // POST: /Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                SaveImage(model, Request);
                var productRepository = new ProductRepository();
                productRepository.Create(model.Product);
                productRepository.Save();
            }
            model.Colors = new ProductColors().Colors;
            model.Brands = BrandDictionary();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                SaveImage(model, Request);
                var productRepository = new ProductRepository();
                productRepository.Update(model.Product);
                productRepository.Save();
            }
            model.Colors = new ProductColors().Colors;
            model.Brands = BrandDictionary();
            return View("Create", model);
        }


      
        public FileContentResult GetImage(Product product)
        {
            if (product != null)
            {
                return File(product.Image, product.ImageType);
            }
            else
            {
                return null;
            }
        }

        public JsonResult GetBrandsList()
        {
            return Json(new BrandRepository().GetList().ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsBrandUsing(int id)
        {
            //тут все очень плохо (я подумаю как исправить)
            using (ProductContext db = new ProductContext())
            {
                try
                {
                    var prod = db.Products.First(p => p.BrandId == id);
                    return Json(prod != null, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }    
            }
        }
        private Dictionary<int, string> BrandDictionary()
        {
            var brandsRepository = new BrandRepository();
            return brandsRepository.GetList().ToDictionary(b => b.BrandId, b => b.BrandName);
       
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