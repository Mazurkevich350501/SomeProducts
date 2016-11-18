﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.Models.Create;
using SomeProducts.PresentationServices.PresentationServices.Create;

namespace SomeProducts.PresentationServices.Test
{
    [TestClass]
    public class ProductViewModelPresentationServicesTest
    {
        private ProductViewModelPresentationService _productService;
        private Mock<IProductDao> _productDao;
        private Mock<IBrandDao> _brandDao;
        private Mock<IUserHelper> _userHelper;
        private ProductViewModel _productViewModel;
        private Product _product;
        private List<Brand> _brandList;
        private Dictionary<string, string> _colors;
        private const int CompanyId = 3;
        [TestInitialize]
        public void TestInitialize()
        {
            _productDao = new Mock<IProductDao>();
            _brandDao = new Mock<IBrandDao>();
            _userHelper = new Mock<IUserHelper>();
            _productService = new ProductViewModelPresentationService(
                _productDao.Object,
                _brandDao.Object,
                _userHelper.Object);

            _product = new Product()
            {
                Id = 5,
                Name = "Name",
                Brand = new Brand() {Id = 5, Name = "BrName"},
                Quantity = 5,
                Color = "#ffffff",
                Description = "Description",
                ImageType = "ImageType",
                Image = new byte[] { 1, 2, 3 }
            };

            _brandList = new List<Brand>
            {
                new Brand() {Id = 1, Name = "name1", CompanyId = CompanyId},
                new Brand() {Id = 2, Name = "name2", CompanyId = CompanyId}
            };

            _colors = ProductColors.Colors;

            _productViewModel = new ProductViewModel()
            {
                Product = new ProductModel()
                {
                    Id = 5,
                    Name = "Name",
                    BrandId = 5,
                    Quantity = 5,
                },
                Colors = _colors
            };
            _userHelper.Setup(d => d.GetCompany()).Returns(CompanyId);
        }

        private static void ProductModelAssert(Product expected, ProductModel actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.BrandId, actual.BrandId);
            Assert.AreEqual(expected.Color, actual.Color);
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.ImageType, actual.ImageType);
            Assert.AreEqual(expected.Image, actual.Image);
        }

        [TestMethod]
        public void GetEmptyProductViewModel_Should_Return_Emty_ProductViewModel()
        {
            _brandDao.Setup(d => d.GetCompanyBrands(CompanyId)).Returns(_brandList);
            
            var result = _productService.GetEmptyProductViewModel();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_colors, result.Colors);
            CollectionAssert.AreEqual(_brandList.ToDictionary(b => b.Id, b => b.Name), result.Brands);
            ProductModelAssert(new Product(), result.Product);
        }

        [TestMethod]
        public void GetProductViewModel_Should_Return_ProductViewModel()
        {
            _brandDao.Setup(d => d.GetCompanyBrands(It.IsAny<int>())).Returns(_brandList);
            _productDao.Setup(d => d.GetProduct(CompanyId, _product.Id)).Returns(_product);

            var result = _productService.GetProductViewModel(_product.Id);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_colors, result.Colors);
            CollectionAssert.AreEqual(_brandList.ToDictionary(b => b.Id, b => b.Name), result.Brands);
            ProductModelAssert(_product, result.Product);
        }

        [TestMethod]
        public void GetLastProductViewMode_Should_Return_Filled_ProductViewModel()
        {
            _brandDao.Setup(d => d.GetCompanyBrands(CompanyId)).Returns(_brandList);
            _productDao.Setup(d => d.GetLastProduct()).Returns(_product);

            var result = _productService.GetLastProductViewMode();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_colors, result.Colors);
            CollectionAssert.AreEqual(_brandList.ToDictionary(b => b.Id, b => b.Name), result.Brands);
            ProductModelAssert(_product, result.Product);
        }

        [TestMethod]
        public void UpdateProductViewModel_Should_Add_CreateDate()
        {
            var createDate = new DateTime();
            _productDao.Setup(d => d.UpdateProduct(It.IsAny<Product>()))
                .Callback<Product>((product) =>
                {
                    createDate = product.CreateDate;
                });

            _productService.UpdateProductViewModel(_productViewModel);

            Assert.IsTrue(DateTime.Compare(createDate, DateTime.Now.Subtract(TimeSpan.FromSeconds(10))) < 0);
        }

        [TestMethod]
        public void UpdateProductViewModel_Should_Return_False_If_Not_Updated()
        {
            _productDao.Setup(d => d.UpdateProduct(It.IsAny<Product>())).Returns(false);
            
            var result = _productService.UpdateProductViewModel(_productViewModel);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateProductViewModel_Should_Return_True_If_Updated()
        {
            _productDao.Setup(d => d.UpdateProduct(It.IsAny<Product>())).Returns(true);

            var result = _productService.UpdateProductViewModel(_productViewModel);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateProductViewModel_Should_Create_Product()
        {
            var product = new Product();
            _productDao.Setup(d => d.CreateProduct(It.IsAny<Product>()))
                .Callback<Product>(p => product = p);

            _productService.CreateProductViewModel(_productViewModel);

            ProductModelAssert(product, _productViewModel.Product);
        }

        [TestMethod]
        public void RemoveProductViewModel_Should_Remove_Product()
        {
            var removedId = 0;
            _userHelper.Setup(d => d.GetSuperAdminCompany()).Returns(CompanyId);
            _productDao.Setup(d => d.RemoveProduct(It.IsAny<Product>()))
                .Callback<Product>(p => removedId = p.Id);
            _productDao.Setup(d => d.GetProduct(CompanyId, It.IsAny<int>()))
                .Returns(_product);
            _productService.RemoveProductViewModel(_product.Id);

            Assert.AreEqual(_product.Id, removedId);
        }
    }
}
