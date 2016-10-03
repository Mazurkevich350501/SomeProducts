﻿
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.Models;
using SomeProducts.PresentationServices.Models.Brand;
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
                new Brand() {Id = 1, Name = "Name1"},
                new Brand() {Id = 2, Name = "Name2"}
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
                    new BrandModel() {Id = 0, Name = "Name01"},
                    new BrandModel() {Id = 0, Name = "Name02"}
                },
                RemovedBrands = new List<BrandModel>()
                {
                    new BrandModel() {Id = 1, Name = "Name1"},
                    new BrandModel() {Id = 2, Name = "Name2"},
                    new BrandModel() {Id = 3, Name = "Name3"}
                }
            };
            var addedList = new List<BrandModel>();
            var removedIdList = new List<int>();

            _brandDao.Setup(d => d.CreateBrand(It.IsAny<Brand>()))
                .Callback<Brand>(brand =>
                {
                    if (brand.Id == 0)
                    {
                        addedList.Add(new BrandModel()
                        {
                            Id = brand.Id,
                            Name = brand.Name
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
