using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Repository.Interface;


namespace SomeProducts.DAL.Repository
{
    public class DateModifiedRepository<TEntity> : IDateModifiedRepository<TEntity> 
        where TEntity : class, IDateModified, IIdentify, IAvailableCompany
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

        public TEntity GetCompanyItem(int companyId, int itemId)
        {
            return GetCompanyItems(companyId).FirstOrDefault(i => i.Id == itemId);
        }

        public IQueryable<TEntity> GetCompanyItems(int companyId)
        {
            return _db.Set<TEntity>().Where(i => i.CompanyId == companyId);
        }
        
        public TEntity GetLast(int companyId)
        {
            return GetCompanyItems(companyId).OrderByDescending(t => t.CreateDate).FirstOrDefault();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public bool Update(TEntity item)
        {
            var oldItem = GetById(item.Id);
            var lastItemVersion = oldItem.RowVersion;
            if (oldItem.CompanyId != item.CompanyId)
            {
                throw new WarningException("Item.CompanyId can`t be modified.");
            }

            if (lastItemVersion.SequenceEqual(item.RowVersion))
            {
                item.CreateDate = oldItem.CreateDate;
                item.ModifiedDate = DateTime.UtcNow;
                _db.Set<TEntity>().AddOrUpdate(item);
                return true;
            }
            return false;
        }

       
    }
}