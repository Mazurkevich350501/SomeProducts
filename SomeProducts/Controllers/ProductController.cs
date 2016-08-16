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
            ProductColors color = new ProductColors();
            var model = new ProductViewModel()
            {
                Product = new Product { Name = "a", Description = "s", Quantity = 10, Color = "@ffff00", BrandId = 2 },
                Brands = new Dictionary<int, string>(),
                Colors = color.Colors 
                   
            };
            /*using (var db = new ProductContext())
            {
                var brands = db.Brands;
                foreach(Brand brand in brands)
                {
                    model.Brands.Add(brand.BrandId, brand.BrandName);
                }
            }*/
            model.Brands.Add(2, "asdasdasdasd");
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new ProductContext())
            {
                ViewBag.ProductModel = db.Products.First(x => x.ProductId == id);
            }
            if (ViewBag.ProductModel == null)
            {
                return null;
            }    
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var image = Request.Files[0];
                        if (image != null && image.ContentLength > 0)
                        {
                            model.Product.Image = new byte[image.ContentLength];
                            image.InputStream.Read(model.Product.Image, 0, image.ContentLength);
                            model.Product.ImageType = image.ContentType;

                        }
                    }
                    using (var db = new ProductContext())
                    {
                        db.Products.Add(model.Product);
                        db.SaveChanges();
                    }
                }
                return null;//View(model);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Product model)
        {
            
            return null;
        }
      
        public FileContentResult GetImage(int productId)
        {
            Product product;
            using (var db = new ProductContext())
            {
                product = db.Products.FirstOrDefault(p => p.ProductId == productId);
            }

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
            return Json(new { foo = "bar", baz = "Blech" }, JsonRequestBehavior.AllowGet);
        }
        public bool IsBrandUsing(int brandId)
        {
            return true;
        }
    }
}