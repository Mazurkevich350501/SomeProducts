using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Test
{
    [TestClass]
    public class ProductDaoTest
    {
        private Mock<IRepository<Product>> _productRepository;
        private ProductDao _productDao;
        private Product _product;

        [TestInitialize]
        public void TestInitialize()
        {
            _productRepository = new Mock<IRepository<Product>>();
            _productDao = new ProductDao(_productRepository.Object);
            _product = new Product()
            {
                ProductId = 5,
                Name = "Name",
                BrandId = 5,
                Quantity = 5,
                Color = "#ffffff",
                Description = "Description",
                ImageType = "ImageType",
                Image = new byte[] { 1, 2, 3 },
                CreateDate = DateTime.Now
            };
        }

        [TestMethod]
        public void UpdateProduct_Should_Update_Product_Data()
        {
            var updatedProduct = new Product();
            _productRepository.Setup(r => r.Update(It.IsAny<Product>()))
                .Callback<Product>(p => updatedProduct = p);

            _productDao.UpdateProduct(_product);

            Assert.AreEqual(_product, updatedProduct);
        }

        [TestMethod]
        public void GetProduct_Should_Return_Product_By_Id()
        {
            _productRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(_product);

            var result = _productDao.GetProduct(_product.ProductId);

            Assert.AreEqual(_product, result);
        }

        [TestMethod]
        public void GetLastProduct_Should_Return_Last_Product()
        {
            _productRepository.Setup(r => r.GetLast()).Returns(_product);

            var result = _productDao.GetLastProduct();

            Assert.AreEqual(_product, result);
        }

        [TestMethod]
        public void RemoveProduct_Should_Remove_Product_By_Id()
        {
            var deletedId = 0;
            _productRepository.Setup(r => r.Delete(It.IsAny<int>()))
                .Callback<int>(id => deletedId = id);

            _productDao.RemoveProduct(_product.ProductId);

            Assert.AreEqual(_product.ProductId, deletedId);
        }

        [TestMethod]
        public void CreateProduct_Should_Create_New_Product()
        {
            var product = new Product();
            _productRepository.Setup(r => r.Create(It.IsAny<Product>()))
                .Callback<Product>(p => product = p);

            _productDao.CreateProduct(_product);

            Assert.AreEqual(_product, product);
        }

        [TestMethod]
        public void GetCreateTime_Should_Return_Creation_Date()
        {
            _productRepository.Setup(r => r.GetCreateTime(It.IsAny<int>()))
                .Returns(_product.CreateDate);

            var result = _productDao.GetCreateTime(_product.ProductId);

            Assert.AreEqual(_product.CreateDate, result);
        }
    }
}
