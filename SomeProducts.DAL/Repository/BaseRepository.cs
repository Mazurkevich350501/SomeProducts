using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using SomeProducts.DAL.Context;


namespace SomeProducts.DAL.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDateModified, IIdentify
    {
        private readonly ProductContext _db;

        public BaseRepository(ProductContext db)
        {
            _db = db;
        }

        public BaseRepository(string connection)
        {
            _db = new ProductContext(connection);
        }

        public void Create(TEntity item)
        {
            item.CreateDate = DateTime.UtcNow;
            _db.Set<TEntity>().Add(item);
        }

        public void Delete(TEntity item)
        {
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

        public bool Update(TEntity item)
        {
            var lastItemVersion = GetById(item.Id).RowVersion;
            if (lastItemVersion.SequenceEqual(item.RowVersion))
            {
                item.ModifiedDate = DateTime.UtcNow;
                _db.Set<TEntity>().AddOrUpdate(item);
                return true;
            }
            return false;
        }
    }
}