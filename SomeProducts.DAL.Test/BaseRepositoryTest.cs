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
        private static DateModifiedRepository<Brand> _repository;
        private static Brand _brand;
        private const int CompanyId = 1;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _brand = new Brand() { Name = "name", CompanyId = CompanyId};
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory));
            _repository = new DateModifiedRepository<Brand>("name=Test");
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            var brands = _repository.GetAllItems();
            foreach (var brand in brands)
            {
                _repository.Delete(brand);
            }
            _repository.Save();
        }

        [TestMethod]
        public void Save_Should_Save_Item_To_Datebase()
        {
            _repository.Create(_brand);
            _repository.Save();

            var result = _repository.GetLast(CompanyId);

            Assert.IsNotNull(_brand);
            Assert.AreEqual(_brand.Name, result.Name);
        }

        [TestMethod]
        public void Save_Should_Set_CreateDate()
        {
            _repository.Create(_brand);
            _repository.Save();

            var result = _repository.GetLast(CompanyId);
            
            Assert.IsTrue(DateTime.Compare(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)), result.CreateDate) < 0);
        }

        [TestMethod]
        public void GetAll_Should_Return_AllItems()
        {
            var brands = new List<Brand>()
            {
                new Brand() {Name = "name1"},
                new Brand() {Name = "name2"},
                new Brand() {Name = "name3"}
            };
            foreach (var brand in brands)
            {
                _repository.Create(brand);
            }
            _repository.Save();

            var result = _repository.GetAllItems().ToList();

            foreach (var brand in brands)
            {
                Assert.AreEqual(brand.Name, result.Find( b => b.Name == brand.Name).Name);   
            }
        }

        [TestMethod]
        public void Update_Should_Save_Item_Changes()
        {
            _repository.Create(_brand);
            _repository.Save();
            var brand = _repository.GetLast(CompanyId);
            brand.Name = "name2";

            _repository.Update(brand);
            _repository.Save();
            var result = _repository.GetById(brand.Id);

            Assert.AreEqual(brand.Name, result.Name);
        }

        [TestMethod]
        public void Update_Should_Set_UpdateDate()
        {
            _repository.Create(_brand);
            _repository.Save();
            var brand = _repository.GetLast(CompanyId);
            brand.Name = "name2";

            _repository.Update(brand);
            _repository.Save();
            var result = _repository.GetById(brand.Id);

            Assert.IsNotNull(result.ModifiedDate);
            Assert.IsTrue(DateTime.Compare(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)), result.ModifiedDate.Value) < 0);
        }
    }
}
