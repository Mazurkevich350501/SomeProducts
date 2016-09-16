using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Test
{
    [TestClass]
    public class BrandDaoTest
    {
        private Mock<IRepository<Brand>> _brandRepository;
        private Mock<IRepository<Product>> _productRepository;
        private BrandDao _brandDao;
        private Brand _brand;

        [TestInitialize]
        public void TestInitialize()
        {
            _brandRepository = new Mock<IRepository<Brand>>();
            _productRepository = new Mock<IRepository<Product>>();
            _brandDao = new BrandDao(_brandRepository.Object, _productRepository.Object);
            _brand = new Brand()
            {
                Id = 5,
                Name = "name",
                CreateDate = DateTime.Now
            };
        }

        [TestMethod]
        public void RemoveBrand_Should_Remove_Brand_By_Id()
        {
            var deletedId = 0;
            _brandRepository.Setup(r => r.Delete(It.IsAny<int>()))
                .Callback<int>(id => deletedId = id);

            _brandDao.RemoveBrand(_brand.Id);

            Assert.AreEqual(_brand.Id, deletedId);
        }

        [TestMethod]
        public void CreateBrand_Should_Create_New_Brand()
        {
            var brand = new Brand();
            _brandRepository.Setup(r => r.Create(It.IsAny<Brand>()))
                .Callback<Brand>(p => brand = p);

            _brandDao.CreateBrand(_brand);

            Assert.AreEqual(_brand, brand);
        }

        [TestMethod]
        public void GetAllItems_Should_Return_All_Brands()
        {
            var brandList = new List<Brand>() { _brand, _brand, _brand };
            _brandRepository.Setup(r => r.GetAllItems())
                .Returns(brandList);

            var result = _brandDao.GetAllItems();

            CollectionAssert.AreEqual(brandList, result.ToList());
        }

        [TestMethod]
        public void IsBrandUsing_Should_Return_True_If_Any_Product_Contain_This_Brand()
        {
            var productList = new List<Product>
            {
                new Product() {BrandId = 1},
                new Product() {BrandId = 2}
            };
            _productRepository.Setup(r => r.GetAllItems())
                .Returns(productList);

            var trueResult = _brandDao.IsBrandUsing(2);
            var falseResult = _brandDao.IsBrandUsing(3);

            Assert.IsTrue(trueResult);
            Assert.IsFalse(falseResult);
        }
    }
}
