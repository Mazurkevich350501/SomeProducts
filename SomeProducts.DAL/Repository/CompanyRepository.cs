
using System.Data.Entity.Migrations;
using System.Linq;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Models.ModelState;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Repository
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly ProductContext _db;

        public CompanyRepository(ProductContext db)
        {
            _db = db;
        }

        public CompanyRepository(string connection)
        {
            _db = new ProductContext(connection);
        }

        ~CompanyRepository()
        {
            Dispose();
        }

        public Company Create(Company item)
        {
            return _db.Set<Company>().Add(item);
        }

        public void Delete(Company item)
        {
            item.ActiveStateId = State.Disable;
            Update(item);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IQueryable<Company> GetAllItems()
        {
            return _db.Set<Company>()
                .Where(i => i.ActiveStateId == State.Active);
        }

        public Company GetById(int id)
        {
            var item = _db.Set<Company>().Find(id);
            return item.ActiveStateId == State.Active
                ? item
                : null;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public bool Update(Company item)
        {
            _db.Set<Company>().AddOrUpdate(item);
            return true;
        }
    }
}
