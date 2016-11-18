using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        public ProductContext Db { get;}

        public BaseRepository(ProductContext db)
        {
            Db = db;
        }

        ~BaseRepository()
        {
            Dispose();
        }

        public TEntity Create(TEntity item)
        {
            return Db.Set<TEntity>().Add(item);
        }

        public void Delete(TEntity item)
        {
            Db.Set<TEntity>().Remove(item);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public IQueryable<TEntity> GetAllItems()
        {
            return Db.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity GetCompanyItem(int companyId, int itemId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetCompanyItems(int companyId)
        {
            throw new NotImplementedException();
        }

        public TEntity GetLast(int companyId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            Db.SaveChanges();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity item)
        {
            Db.Set<TEntity>().AddOrUpdate(item);
            return true;
        }
    }
}