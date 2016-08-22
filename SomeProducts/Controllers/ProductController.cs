﻿using SomeProducts.Repository;
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
                Brands = CreateBrandDictionary(),
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
                Brands = CreateBrandDictionary(),
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
                var product = productRepository.GetAllItems().LastOrDefault(p => p.ProductId != 0);
                return Redirect(Url.Action("Edit", "Product", new { id = product.ProductId }));
            }
            model.Colors = new ProductColors().Colors;
            model.Brands = CreateBrandDictionary();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                SaveImage(model, Request);
                var productRepository = new ProductRepository();
                productRepository.Update(model.Product);
                productRepository.Save();
            }
            model.Colors = new ProductColors().Colors;
            model.Brands = CreateBrandDictionary();
            return View("Create", model);
        }

        public JsonResult SaveBrandsChanges(BrandsChangeModel changeModel)
        {
            if(changeModel != null)
            {
                var repository = new BrandRepository();
                if(changeModel.RemovedBrands != null)
                {
                    foreach (Brand brand in changeModel.RemovedBrands)
                    {
                        repository.Delete(brand.BrandId);
                    }
                }
                if(changeModel.AddedBrands != null)
                {
                    foreach (Brand brand in changeModel.AddedBrands)
                    {
                        repository.Create(brand);
                    }
                }
                repository.Save();
            }
            return GetBrandsList();
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
        [HttpPost]
        public JsonResult Delete(int productId)
        {
            /*var repository = new ProductRepository();
            repository.Delete(id);
            repository.Save();*/
            return Json(Url.Action("Create", "Product"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBrandsList()
        {
            return Json(new BrandRepository().GetAllItems().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBrandUsing(int id)
        {
            using (ProductContext db = new ProductContext())
            {
                return Json(db.Products.Any(p => p.BrandId == id) , JsonRequestBehavior.AllowGet);  
            }
        }

        private Dictionary<int, string> CreateBrandDictionary()
        {
            var brandsRepository = new BrandRepository();
            return brandsRepository.GetAllItems().ToDictionary(b => b.BrandId, b => b.BrandName);
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