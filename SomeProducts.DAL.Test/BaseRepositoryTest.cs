using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Test
{
# if DEBUG
    [TestClass]
# endif
    public class BaseRepositoryTest
    {
        private static BaseRepository<Brand> _repository;
        private static Brand _brand;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _brand = new Brand() { BrandName = "name" };
            _repository = new BaseRepository<Brand>("TestConnection");
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            var brands = _repository.GetAllItems();
            foreach (var brand in brands)
            {
                _repository.Delete(brand.BrandId);
            }
            _repository.Save();
        }

        [TestMethod]
        public void Save_Should_Save_Item_To_Datebase()
        {
            _repository.Create(_brand);
            _repository.Save();

            var result = _repository.GetLast();

            Assert.IsNotNull(_brand);
            Assert.AreEqual(_brand.BrandName, result.BrandName);
        }

        [TestMethod]
        public void Save_Should_Set_CreateDate()
        {
            _repository.Create(_brand);
            _repository.Save();

            var result = _repository.GetLast();
            
            Assert.IsTrue(DateTime.Compare(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)), result.CreateDate) < 0);
        }

        [TestMethod]
        public void GetAll_Should_Return_AllItems()
        {
            var brands = new List<Brand>()
            {
                new Brand() {BrandName = "name1"},
                new Brand() {BrandName = "name2"},
                new Brand() {BrandName = "name3"}
            };
            foreach (var brand in brands)
            {
                _repository.Create(brand);
            }
            _repository.Save();

            var result = _repository.GetAllItems().ToList();

            foreach (var brand in brands)
            {
                Assert.AreEqual(brand.BrandName, result.Find( b => b.BrandName == brand.BrandName).BrandName);   
            }
        }

        [TestMethod]
        public void Update_Should_Save_Item_Changes()
        {
            _repository.Create(_brand);
            _repository.Save();
            var brand = _repository.GetLast();
            brand.BrandName = "name2";

            _repository.Update(brand);
            _repository.Save();
            var result = _repository.GetById(brand.BrandId);

            Assert.AreEqual(brand.BrandName, result.BrandName);
        }

        [TestMethod]
        public void Update_Should_Set_UpdateDate()
        {
            _repository.Create(_brand);
            _repository.Save();
            var brand = _repository.GetLast();
            brand.BrandName = "name2";

            _repository.Update(brand);
            _repository.Save();
            var result = _repository.GetById(brand.BrandId);

            Assert.IsNotNull(result.ModifiedDate);
            Assert.IsTrue(DateTime.Compare(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)), result.ModifiedDate.Value) < 0);
        }

        [TestMethod]
        public void GetCreatTime_Should_Return_CreateDate()
        {
            _repository.Create(_brand);
            _repository.Save();

            var brand = _repository.GetLast();
            var result = _repository.GetCreateTime(brand.BrandId);

            Assert.AreEqual(brand.CreateDate, result);
        }
    }
}
