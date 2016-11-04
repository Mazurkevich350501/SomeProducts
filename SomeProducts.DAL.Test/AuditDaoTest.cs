

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Test
{
    [TestClass]
    public class AuditDaoTest
    {
        private Mock<IRepository<AuditItem>> _repository;
        private AuditDao _dao;
        private readonly Product[] _items;
        private readonly Brand[] _brandItems;

        public AuditDaoTest()
        {
            _items = new [] {
                new Product()
                {
                    BrandId = 1,
                    Color = "asd",
                    CompanyId = 3,
                    Description = "asd",
                    Id = 1,
                    Name = "asd",
                    Quantity = 5
                },
                new Product()
                {
                    BrandId = 1,
                    Color = "assd",
                    CompanyId = 3,
                    Id = 1,
                    Name = "asd",
                    Quantity = 3
                }
            };
             _brandItems = new []{
                new Brand()
                {
                    CompanyId = 3,
                    Id = 1,
                    Name = "asd",
                },
                new Brand()
                {
                    CompanyId = 2,
                    Id = 1,
                    Name = "asdss",
                }
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new Mock<IRepository<AuditItem>>();
            _dao = new AuditDao(_repository.Object);
        }

        [TestMethod]
        public void CreateAudititem_Should_Work_Without_Exceptions()
        {
            _dao.CreateEditAuditItems(_brandItems[0], _brandItems[1], 3);
            _dao.CreateEditAuditItems(_items[0], _items[1], 3);
        }
    }
}
