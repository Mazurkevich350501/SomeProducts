using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Test
{
    [TestClass]
    public class BrandDaoTest
    {
        private Mock<IDateModifiedRepository<Brand>> _brandRepository;
        private Mock<IDateModifiedRepository<Product>> _productRepository;
        private Mock<IAuditDao> _auditDao;
        private Mock<IUserHelper> _userHelper;
        private BrandDao _brandDao;
        private Brand _brand;
        private const int CompanyId = 1;

        [TestInitialize]
        public void TestInitialize()
        {
            _brandRepository = new Mock<IDateModifiedRepository<Brand>>();
            _productRepository = new Mock<IDateModifiedRepository<Product>>();
            _userHelper = new Mock<IUserHelper>();
            _auditDao = new Mock<IAuditDao>();
            _brandDao = new BrandDao(
                _brandRepository.Object, 
                _productRepository.Object, 
                _auditDao.Object, 
                _userHelper.Object);
            _brand = new Brand()
            {
                Id = 5,
                Name = "name",
                CreateDate = DateTime.Now,
                CompanyId = CompanyId
            };
        }

        [TestMethod]
        public void RemoveBrand_Should_Remove_Brand_By_Id()
        {
            var deletedId = 0;
            _brandRepository.Setup(r => r.Delete(It.IsAny<Brand>()))
                .Callback<Brand>(brand => deletedId = brand.Id);

            _brandDao.RemoveBrand(_brand);

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
        public void GetCompanyBrands_Should_Return_All_Company_Brands()
        {
            var brandList = new List<Brand>() { _brand, _brand, _brand };
            _brandRepository.Setup(r => r.GetCompanyItems(It.IsAny<int>()))
                .Returns(brandList.AsQueryable());

            var result = _brandDao.GetCompanyBrands(CompanyId);

            CollectionAssert.AreEqual(brandList, result.ToList());
        }

        [TestMethod]
        public void IsBrandUsing_Should_Return_True_If_Any_Product_Contain_This_Brand()
        {
            var productList = new List<Product>
            {
                new Product() {Brand = new Brand() {Id = 1, Name = "name1", CompanyId = CompanyId}, CompanyId = CompanyId}, 
                new Product() {Brand = new Brand() {Id = 2, Name = "name2", CompanyId = CompanyId}, CompanyId = CompanyId}
            };
            _productRepository.Setup(r => r.GetCompanyItems(It.IsAny<int>()))
                .Returns(productList.AsQueryable());

            var trueResult = _brandDao.IsBrandUsing(CompanyId);
            var falseResult = _brandDao.IsBrandUsing(CompanyId);

            Assert.IsTrue(trueResult);
            Assert.IsFalse(falseResult);
        }
    }
}
