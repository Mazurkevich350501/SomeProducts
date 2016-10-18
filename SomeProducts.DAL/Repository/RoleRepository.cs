using System;
using System.Data.Entity.Migrations;
using System.Linq;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Repository
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly ProductContext _db;

        public RoleRepository(ProductContext db)
        {
            _db = db;
        }

        ~RoleRepository()
        {
            Dispose();
        }

        public void Create(Role role)
        {
            _db.Roles.Add(role);
        }

        public void Delete(Role role)
        {
            _db.Roles.Remove(_db.Roles.Find(role));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IQueryable<Role> GetAllItems()
        {
            return _db.Roles.AsQueryable();
        }

        public Role GetById(int id)
        {
            return _db.Roles.Find(id);
        }

        public DateTime GetCreateTime(int id)
        {
            throw new NotImplementedException();
        }

        public Role GetLast()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public bool Update(Role role)
        {
            _db.Roles.AddOrUpdate(role);
            return true;
        }
    }
}
