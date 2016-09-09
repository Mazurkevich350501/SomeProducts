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
        [TestMethod]
        public void GetAllItems_Should_Return_All_Brands()
        {
            var brandDao = new Mock<IBrandDao>();
            var service = new BrandModelPresentationService(brandDao.Object);
            var brands = new List<Brand>
            {
                new Brand() {BrandId = 1, BrandName = "Name1"},
                new Brand() {BrandId = 2, BrandName = "Name2"}
            };
            brandDao.Setup(d => d.GetAllItems()).Returns(brands);

            var result = service.GetAllItems();

            Assert.IsNotNull(result);
            Assert.AreEqual(brands.Count, result.ToList().Count);
        }
    }
}
