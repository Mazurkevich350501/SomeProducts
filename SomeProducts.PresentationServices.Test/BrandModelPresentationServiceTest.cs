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
    public class BrandModelPresentationServiceTest
    {
        private Mock<IBrandDao> _brandDao;
        private BrandModelPresentationService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _brandDao = new Mock<IBrandDao>();
            _service = new BrandModelPresentationService(_brandDao.Object);
        }
        
        [TestMethod]
        public void GetAllItems_Should_Return_All_Brands()
        {
            var brands = new List<Brand>
            {
                new Brand() {BrandId = 1, BrandName = "Name1"},
                new Brand() {BrandId = 2, BrandName = "Name2"}
            };
            _brandDao.Setup(d => d.GetAllItems()).Returns(brands);

            var result = _service.GetAllItems();

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
                    new BrandModel() {BrandId = 0, BrandName = "Name01"},
                    new BrandModel() {BrandId = 0, BrandName = "Name02"}
                },
                RemovedBrands = new List<BrandModel>()
                {
                    new BrandModel() {BrandId = 1, BrandName = "Name1"},
                    new BrandModel() {BrandId = 2, BrandName = "Name2"},
                    new BrandModel() {BrandId = 3, BrandName = "Name3"}
                }
            };
            var addedList = new List<BrandModel>();
            var removedIdList = new List<int>();

            _brandDao.Setup(d => d.CreateBrand(It.IsAny<Brand>()))
                .Callback<Brand>((Brand brand) =>
                {
                    if (brand.BrandId == 0)
                    {
                        addedList.Add(new BrandModel()
                        {
                            BrandId = brand.BrandId,
                            BrandName = brand.BrandName
                        });
                    }
                });
            _brandDao.Setup(d => d.RemoveBrand(It.IsAny<int>()))
                .Callback<int>((int id) =>
                {
                    if (id > 0 && id < 4)
                    {
                        removedIdList.Add(id);
                    }
                });

            _service.SaveBrandChanges(changeModel);

            foreach (var brand in changeModel.RemovedBrands)
            {
                Assert.AreEqual(brand.BrandId, removedIdList.Find(id => id == brand.BrandId));
            }
            foreach (var brand in changeModel.AddedBrands)
            {
                Assert.AreEqual(brand.BrandName, addedList.Find(b => b.BrandName == brand.BrandName).BrandName);
            }
        }
    }
}
