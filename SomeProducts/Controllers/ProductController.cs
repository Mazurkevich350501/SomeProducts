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
        public ActionResult Create(int id)
        {
            ViewBag.ProductModel = null;
            return View();
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
        public ActionResult Create(int id, Product model, HttpPostedFileBase image)
        {
            if (IsModelCorrectly(model))
            {
                if (image != null)
                {
                    model.ImageType = image.ContentType;
                    model.Image = new byte[image.ContentLength];
                    image.InputStream.Read(model.Image, 0, image.ContentLength);
                }
                using (var db = new ProductContext())
                {
                    db.Products.Add(model);
                }
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(int id, Product model)
        {
            if(IsModelCorrectly(model))
            {
                using (var db = new ProductContext())
                {
                    db.Products.Add(model);
                }
            }
            return null;
        }
        
        bool IsModelCorrectly(Product model)
        {
            if (model.Name != null && model.Name.Length < 200)
                if (model.Description != null && model.Description.Length < 200)
                    if (model.Quantity > 0 && model.BrandId > 0)
                        return true;
            return false;
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
    }
}