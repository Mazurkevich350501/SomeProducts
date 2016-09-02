using SomeProducts.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SomeProducts.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDateModified
    {
        private ProductContext db;

        public BaseRepository()
        {
            this.db = new ProductContext();
        }

        public void Create(TEntity item)
        {
            item.CreateDate = DateTime.Now;
            db.Set<TEntity>().Add(item);
        }

        public void Delete(int id)
        {
            TEntity item = db.Set<TEntity>().Find(id);
            if (item != null)
                db.Set<TEntity>().Remove(item);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> GetAllItems()
        {
            return db.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(TEntity item)
        {
            item.ModifiedDate = DateTime.Now;
            db.Entry(item).State = EntityState.Modified;
        }
    }
}