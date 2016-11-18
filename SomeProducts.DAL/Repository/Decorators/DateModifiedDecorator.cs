using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Repository.Interface;
using SomeProducts.DAL.Repository.Interface.Model;

namespace SomeProducts.DAL.Repository.Decorators
{
    public class DateModifiedDecorator<TEntity> : IRepository<TEntity>
        where TEntity : class, IDateModified, IAvailableCompany
    {
        private readonly IRepository<TEntity> _repository;

        public ProductContext Db => _repository.Db;

        public DateModifiedDecorator(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        
        public  TEntity Create(TEntity item)
        {
            item.CreateDate = DateTime.UtcNow;
            return _repository.Create(item);
        }
        
        public  void Delete(TEntity item)
        {
            _repository.Delete(item);
        }
        
        public  IQueryable<TEntity> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public  TEntity GetById(int id)
        {
             return _repository.GetById(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public TEntity GetCompanyItem(int companyId, int itemId)
        {
            return _repository.GetCompanyItem(companyId, itemId);
        }

        public IQueryable<TEntity> GetCompanyItems(int companyId)
        {
            return _repository.GetCompanyItems(companyId);
        }

        public TEntity GetLast(int companyId)
        {
            return _repository.GetAllItems()
                .Where(t => t.CompanyId == companyId)
                .OrderByDescending(t => t.CreateDate)
                .FirstOrDefault();
        }

        public  void Save()
        {
            _repository.Save();
        }

        public async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }

        public  bool Update(TEntity item)
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
                Db.Set<TEntity>().AddOrUpdate(item);
                return true;
            }
            return false;
        }
    }
}