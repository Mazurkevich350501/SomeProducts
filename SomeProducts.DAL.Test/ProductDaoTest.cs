using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.DAL.Models.ModelState;

namespace SomeProducts.DAL.Test
{
    [TestClass]
    public class ProductDaoTest
    {
        private Mock<IRepository<Product>> _productRepository;
        private Mock<IRepository<Brand>> _brandRepository;
        private Mock<IAuditDao> _auditDao;
        private Mock<IUserHelper> _userHelper;
        private ProductDao _productDao;
        private Product _product;
        private const int CompanyId = 3;

        [TestInitialize]
        public void TestInitialize()
        {
            _productRepository = new Mock<IRepository<Product>>();
            _brandRepository = new Mock<IRepository<Brand>>();
            _auditDao = new Mock<IAuditDao>();
            _userHelper = new Mock<IUserHelper>();
            _productDao = new ProductDao(
                _productRepository.Object,
                _brandRepository.Object,
                _auditDao.Object,
                _userHelper.Object);
            _product = new Product()
            {
                Id = 5,
                Name = "Name",
                Brand = new Brand()
                {
                    Id = 5,
                    Name = "BrName",
                    CompanyId = CompanyId
                },
                Quantity = 5,
                Color = "#ffffff",
                Description = "Description",
                ImageType = "ImageType",
                Image = new byte[] { 1, 2, 3 },
                CreateDate = DateTime.Now, 
                CompanyId = CompanyId, 
                ActiveStateId = State.Active
            };
            _userHelper.Setup((r => r.IsInRole(It.IsAny<UserRole>()))).Returns(true);
        }

        [TestMethod]
        public void UpdateProduct_Should_Update_Product_Data()
        {
            var updatedProduct = _product;
            updatedProduct.Name = "NewName";
            _productRepository.Setup(r => r.Update(It.IsAny<Product>()))
                .Callback<Product>(p => updatedProduct = p);
            _brandRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(_product.Brand);
            _productDao.UpdateProduct(_product);

            Assert.AreEqual(_product, updatedProduct);
        }

        [TestMethod]
        public void GetProduct_Should_Return_Product_By_Id()
        {
            _productRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(_product);

            var result = _productDao.GetProduct(_product.Id);

            Assert.AreEqual(_product, result);
        }

        [TestMethod]
        public void GetLastProduct_Should_Return_Last_Product()
        {
            _productRepository.Setup(r => r.GetLast(CompanyId)).Returns(_product);
            _userHelper.Setup(r => r.GetCompany()).Returns(CompanyId);
            var result = _productDao.GetLastProduct();

            Assert.AreEqual(_product, result);
        }

        [TestMethod]
        public void RemoveProduct_Should_Remove_Product_By_Id()
        {
            var deletedId = 0;
            _productRepository.Setup(r => r.Delete(It.IsAny<Product>()))
                .Callback<Product>(product => deletedId = product.Id);

            _productDao.RemoveProduct(_product);

            Assert.AreEqual(_product.Id, deletedId);
        }

        [TestMethod]
        public void CreateProduct_Should_Create_New_Product()
        {
            var product = new Product();
            _productRepository.Setup(r => r.Create(It.IsAny<Product>()))
                .Callback<Product>(p => product = p);
            _brandRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(_product.Brand);

            _productDao.CreateProduct(_product);

            Assert.AreEqual(_product, product);
        }
    }
}
