
using System.Data.Entity.Migrations;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Context;
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.Repository.Interface;
using System;

namespace SomeProducts.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductContext _db;

        public UserRepository(ProductContext db)
        {
            _db = db;
        }

        ~UserRepository()
        {
            Dispose();
        }

        public User Create(User user)
        {
            return _db.Users.Add(user);
        }

        public void Delete(User user)
        {
            _db.Users.Remove(_db.Users.Find(user.Id));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IQueryable<User> GetAllItems()
        {
            return _db.Users.AsQueryable();
        }

        public User GetById(int id)
        {
           return _db.Users.Find(id);
        }

        public User GetCompanyItem(int companyId, int itemId)
        {
            return GetCompanyItems(companyId).FirstOrDefault(i => i.Id == itemId);
        }

        public IQueryable<User> GetCompanyItems(int companyId)
        {
            return _db.Users.Where(i => i.CompanyId == companyId);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public bool Update(User user)
        {
            _db.Users.AddOrUpdate(user);
            return true;
        }
    }
}
