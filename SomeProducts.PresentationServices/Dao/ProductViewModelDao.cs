

using System;
using SomeProducts.DAL.Dao;
using SomeProducts.PresentationServices.Models;
using SomeProducts.DAL.IDao;
using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IDao;

namespace SomeProducts.PresentationServices.Dao
{
    public class ProductViewModelDao : IProductViewModelDao
    {
        private readonly IProductDao _productService = new ProductDao();
        private readonly IBrandDao _brandService = new BrandDao();

        public void RemoveProductViewModel(int id)
        {
            _productService.RemoveProduct(id);
        }

        public ProductViewModel GetProductViewModel(int? id)
        {
            ProductModel productModel;
            if (id != null)
            {
                var product = _productService.GetProduct(id.Value);
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
                Colors = new ProductColors().Colors
            };
        }

        public void UpdateProductViewModel(ProductViewModel model)
        {
            var product = ProductCast(model);
            product.CreateDate = _productService.GetCreateTime(product.ProductId);
            _productService.UpdateProduct(product);
        }

        public void CreateProductViewModel(ProductViewModel model)
        {
            _productService.CreateProduct(ProductCast(model));
        }

        private Dictionary<int, string> CreateBrandDictionary()
        {
            return _brandService.GetAllItems().ToDictionary(b => b.BrandId, b => b.BrandName);
        }

        private static Product ProductCast(ProductViewModel model)
        {
            if (model != null)
            {
                return new Product()
                {
                    BrandId = model.Product.BrandId,
                    Color = model.Product.Color,
                    Description = model.Product.Description,
                    Image = model.Product.Image,
                    ImageType = model.Product.ImageType,
                    Name = model.Product.Name,
                    ProductId = model.Product.ProductId,
                    Quantity = model.Product.Quantity
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
                    ProductId = model.ProductId,
                    Name = model.Name,
                    BrandId = model.BrandId,
                    Color = model.Color,
                    Description = model.Description,
                    Image = model.Image,
                    ImageType = model.ImageType,
                    Quantity = model.Quantity,
                };
            }
            else
            {
                return null;
            }
        }

        public ProductViewModel GetLastProductViewMode()
        {
            return new ProductViewModel()
            {
                Product = ProductModelCast(_productService.GetLastProduct()),
                Brands = CreateBrandDictionary(),
                Colors = new ProductColors().Colors
            };
        }
    }
}
