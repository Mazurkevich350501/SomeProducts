
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
    public class BrandModelPresentationServiceTest
    {
        private Mock<IBrandDao> _brandDao;
        private Mock<IUserHelper> _userHelper;
        private BrandModelPresentationService _service;
        private const int CompanyId = 1;

        [TestInitialize]
        public void TestInitialize()
        {
            _brandDao = new Mock<IBrandDao>();
            _userHelper = new Mock<IUserHelper>();
            _service = new BrandModelPresentationService(_brandDao.Object, _userHelper.Object);
        }
        
        [TestMethod]
        public void GetAllItems_Should_Return_All_Brands()
        {
            var brands = new List<Brand>
            {
                new Brand() {Id = 1, Name = "Name1", CompanyId = CompanyId},
                new Brand() {Id = 2, Name = "Name2", CompanyId = CompanyId}
            };
            _brandDao.Setup(d => d.GetCompanyBrands(CompanyId))
                .Returns(brands.Where(b => b.CompanyId == CompanyId));

            var result = _service.GetCompanyBrands(CompanyId);

            Assert.IsNotNull(result);
            Assert.AreEqual(brands.Count, result.ToList().Count);
        }

        [TestMethod]
        public void SaveBrandChanges_Should_Save_And_Remove_Items_In_BrandChangeModel()
        {
            var changeModel = new BrandsChangeModel()
            {
                AddedBrands = new List<BrandModel>()
                {
                    new BrandModel() {Id = 0, Name = "Name01", CompanyId = CompanyId},
                    new BrandModel() {Id = 0, Name = "Name02", CompanyId = CompanyId}
                },
                RemovedBrands = new List<BrandModel>()
                {
                    new BrandModel() {Id = 1, Name = "Name1", CompanyId = CompanyId},
                    new BrandModel() {Id = 2, Name = "Name2", CompanyId = CompanyId},
                    new BrandModel() {Id = 3, Name = "Name3", CompanyId = CompanyId}
                }
            };
            var addedList = new List<BrandModel>();
            var removedIdList = new List<int>();

            _brandDao.Setup(d => d.GetById(It.IsAny<int>()))
                .Returns<int>(id =>
                {
                    var brandModel = changeModel.RemovedBrands.First(b => b.Id == id);
                    return new Brand()
                    {
                        CompanyId = brandModel.CompanyId,
                        Id = brandModel.Id,
                        Name = brandModel.Name
                    };
                });

            _brandDao.Setup(d => d.CreateBrand(It.IsAny<Brand>()))
                .Callback<Brand>(brand =>
                {
                    if (brand.Id == 0)
                    {
                        addedList.Add(new BrandModel()
                        {
                            Id = brand.Id,
                            Name = brand.Name,
                            CompanyId = CompanyId
                        });
                    }
                });
            _brandDao.Setup(d => d.RemoveBrand(It.IsAny<Brand>()))
                .Callback<Brand>(brand =>
                {
                    if (brand.Id > 0 && brand.Id < 4)
                    {
                        removedIdList.Add(brand.Id);
                    }
                });

            _service.SaveBrandChanges(changeModel);

            foreach (var brand in changeModel.RemovedBrands)
            {
                Assert.AreEqual(brand.Id, removedIdList.Find(id => id == brand.Id));
            }
            foreach (var brand in changeModel.AddedBrands)
            {
                Assert.AreEqual(brand.Name, addedList.Find(b => b.Name == brand.Name).Name);
            }
        }
    }
}
