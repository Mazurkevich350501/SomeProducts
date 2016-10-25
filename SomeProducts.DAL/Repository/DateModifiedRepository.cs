﻿using System;
using System.Data.Entity.Migrations;
using System.Linq;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Repository.Interface;


namespace SomeProducts.DAL.Repository
{
    public class DateModifiedRepository<TEntity> : IDateModifiedRepository<TEntity> where TEntity : class, IDateModified, IIdentify
    {
        private readonly ProductContext _db;

        public DateModifiedRepository(ProductContext db)
        {
            _db = db;
        }

        public DateModifiedRepository(string connection)
        {
            _db = new ProductContext(connection);
        }
        ~DateModifiedRepository()
        {
            Dispose();
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
            _db.Dispose();
        }

        public IQueryable<TEntity> GetAllItems()
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