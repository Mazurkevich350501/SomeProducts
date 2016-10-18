﻿using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.PresentationServices.Create
{
    public class ProductViewModelPresentationService : IProductViewModelPresentationService
    {
        private readonly IProductDao _productDao;
        private readonly IBrandDao _brandDao;

        public ProductViewModelPresentationService(IProductDao productService, IBrandDao brandService)
        {
            _productDao = productService;
            _brandDao = brandService;
        }

        public void RemoveProductViewModel(int id)
        {
            _productDao.RemoveProduct(_productDao.GetProduct(id));
        }

        public ProductViewModel GetProductViewModel(int? id = null)
        {
            ProductModel productModel;
            if (id != null)
            {
                var product = _productDao.GetProduct(id.Value);
                productModel = ProductModelCast(product);
            }
            else
            {
                productModel = new ProductModel();
            }
            return new ProductViewModel()
            {
                Product = productModel,
                Brands = CreateBrandDictionary(),
                Colors = ProductColors.Colors
            };
        }

        public bool UpdateProductViewModel(ProductViewModel model)
        {
            var product = ProductCast(model.Product);
            product.CreateDate = _productDao.GetCreateTime(product.Id);
            return _productDao.UpdateProduct(product);
        }

        public void CreateProductViewModel(ProductViewModel model)
        {
            _productDao.CreateProduct(ProductCast(model.Product));
        }

        private Dictionary<int, string> CreateBrandDictionary()
        {
            var brands = _brandDao.GetAllItems();
            if (brands != null)
            {
                return brands.ToDictionary(b => b.Id, b => b.Name);
            }
            return new Dictionary<int, string>();
        }

        private static Product ProductCast(ProductModel model)
        {
            if (model != null)
            {
                return new Product()
                {
                    BrandId = model.BrandId,
                    Color = model.Color,
                    Description = model.Description,
                    Image = model.Image,
                    ImageType = model.ImageType,
                    Name = model.Name,
                    Id = model.Id,
                    Quantity = model.Quantity,
                    RowVersion = model.Version
                };
            }
            return null;
        }

        private static ProductModel ProductModelCast(Product model)
        {
            if (model != null)
            {
                return new ProductModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    BrandId = model.BrandId,
                    Color = model.Color,
                    Description = model.Description,
                    Image = model.Image,
                    ImageType = model.ImageType,
                    Quantity = model.Quantity,
                    Version = model.RowVersion
                };
            }
            return null;
        }

        public ProductViewModel GetLastProductViewMode()
        {
            return new ProductViewModel()
            {
                Product = ProductModelCast(_productDao.GetLastProduct()),
                Brands = CreateBrandDictionary(),
                Colors = ProductColors.Colors
            };
        }
    }
}