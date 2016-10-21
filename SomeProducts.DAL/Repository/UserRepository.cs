
using System;
using System.Data.Entity.Migrations;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Context;
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Repository
{
    public class UserRepository : IRepositoryAsync<User>
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

        public void Create(User user)
        {
            _db.Users.Add(user);
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
