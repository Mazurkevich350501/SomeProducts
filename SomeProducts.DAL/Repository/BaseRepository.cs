using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using SomeProducts.DAL.Context;


namespace SomeProducts.DAL.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDateModified
    {
        private readonly ProductContext _db;

        public BaseRepository()
        {
            _db = new ProductContext();
        }

        public void Create(TEntity item)
        {
            item.CreateDate = DateTime.UtcNow;
            _db.Set<TEntity>().Add(item);
        }

        public void Delete(int id)
        {
            var item = _db.Set<TEntity>().Find(id);
            if (item != null)
                _db.Set<TEntity>().Remove(item);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> GetAllItems()
        {
            return _db.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public DateTime GetCreateTime(int id)
        {
            return _db.Set<TEntity>().Find(id).CreateDate;
        }

        public TEntity GetLast()
        {
            return _db.Set<TEntity>().OrderByDescending(t => t.CreateDate).FirstOrDefault();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(TEntity item)
        {
            item.ModifiedDate = DateTime.UtcNow;
            _db.Set<TEntity>().AddOrUpdate(item);
        }
    }
}