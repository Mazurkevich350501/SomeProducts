using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.Models;
using SomeProducts.PresentationServices.PresentaoinServices;

namespace SomeProducts.PresentationServices.Test
{
    [TestClass]
    public class ProductViewModelPresentationServicesTest
    {
        private ProductViewModelPresentationService _productService;
        private Mock<IProductDao> _productDao;
        private Mock<IBrandDao> _brandDao;
        private Product _product;
        private List<Brand> _brandList;
        private Dictionary<string, string> _colors;

        [TestInitialize]
        public void TestInitialize()
        {
            _productDao = new Mock<IProductDao>();
            _brandDao = new Mock<IBrandDao>();
            _productService = new ProductViewModelPresentationService(_productDao.Object, _brandDao.Object);

            _product = new Product()
            {
                ProductId = 5,
                Name = "Name",
                BrandId = 5,
                Quantity = 5,
                Color = "#ffffff",
                Description = "Description",
                ImageType = "ImageType",
                Image = new byte[] { 1, 2, 3 }
            };

            _brandList = new List<Brand>
            {
                new Brand() {BrandId = 1, BrandName = "name1"},
                new Brand() {BrandId = 2, BrandName = "name2"}
            };

            _colors = new ProductColors().Colors;
        }

        private static void ProductModelAssert(Product expected, ProductModel actual)
        {
            Assert.AreEqual(expected.ProductId, actual.ProductId);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.BrandId, actual.BrandId);
            Assert.AreEqual(expected.Color, actual.Color);
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.ImageType, actual.ImageType);
            Assert.AreEqual(expected.Image, actual.Image);
        }

        [TestMethod]
        public void GetProductViewModel_Should_Return_Emty_ProductViewModel_If_Id_Is_Null()
        {
            _brandDao.Setup(d => d.GetAllItems()).Returns(_brandList);

            var result = _productService.GetProductViewModel();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_colors, result.Colors);
            CollectionAssert.AreEqual(_brandList.ToDictionary(b => b.BrandId, b => b.BrandName), result.Brands);
            ProductModelAssert(new Product(), result.Product);
        }

        [TestMethod]
        public void GetProductViewModel_Should_Return_ProductViewModel_If_Id_Is_Not_Null()
        {
            _brandDao.Setup(d => d.GetAllItems()).Returns(_brandList);
            _productDao.Setup(d => d.GetProduct(_product.ProductId)).Returns(_product);
            
            var result = _productService.GetProductViewModel(_product.ProductId);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_colors, result.Colors);
            CollectionAssert.AreEqual(_brandList.ToDictionary(b => b.BrandId, b => b.BrandName), result.Brands);
            ProductModelAssert(_product, result.Product);
        }

        [TestMethod]
        public void GetLastProductViewMode_Should_Return_Filled_ProductViewModel()
        {
            _brandDao.Setup(d => d.GetAllItems()).Returns(_brandList);
            _productDao.Setup(d => d.GetLastProduct()).Returns(_product);

            var result = _productService.GetLastProductViewMode();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_colors, result.Colors);
            CollectionAssert.AreEqual(_brandList.ToDictionary(b => b.BrandId, b => b.BrandName), result.Brands);
            ProductModelAssert(_product, result.Product);
        }
    }
}
