
using System;
using System.Data.Entity.Migrations;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Context;
using System.Linq;

namespace SomeProducts.DAL.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly ProductContext _db;

        public UserRepository(ProductContext db)
        {
            _db = db;
        }

        public void Create(User user)
        {
            _db.Users.Add(user);
        }

        public void Delete(User user)
        {
            _db.Users.Remove(_db.Users.Find(user));
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
        
        public DateTime GetCreateTime(int id)
        {
            throw new NotImplementedException();
        }

        public User GetLast()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public bool Update(User user)
        {
            _db.Users.AddOrUpdate(user);
            return true;
        }
    }
}
