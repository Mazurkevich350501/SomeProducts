
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Repository.Interface;
using SomeProducts.DAL.Repository.Interface.Model;

namespace SomeProducts.DAL.Repository.Decorators
{
    public class CompanyItemsAccessDecorator<TEntity> : IRepository<TEntity>
        where TEntity : class, IAvailableCompany
    {
        private readonly IRepository<TEntity> _repository;

        public ProductContext Db => _repository.Db;

        public CompanyItemsAccessDecorator(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        
        public TEntity Create(TEntity item)
        {
            return _repository.Create(item);
        }

        public void Delete(TEntity item)
        {
            _repository.Delete(item);
        }

        public IQueryable<TEntity> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public TEntity GetCompanyItem(int companyId, int itemId)
        {
            var item = GetById(itemId);
            return item.CompanyId == companyId
                ? item
                : null;
        }

        public IQueryable<TEntity> GetCompanyItems(int companyId)
        {
            return _repository.GetAllItems()
                .Where(i => i.CompanyId == companyId);
        }

        public TEntity GetLast(int companyId)
        {
            return _repository.GetLast(companyId);
        }

        public void Save()
        {
            _repository.Save();
        }

        public async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }

        public bool Update(TEntity item)
        {
            return _repository.Update(item);
        }
    }
}
