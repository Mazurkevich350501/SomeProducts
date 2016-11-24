using System.Linq;
using SomeProducts.DAL.Models.ModelState;
using SomeProducts.DAL.Repository.Interface.Model;
using SomeProducts.DAL.Repository.Interface;
using System.Threading.Tasks;
using SomeProducts.DAL.Context;

namespace SomeProducts.DAL.Repository.Decorators
{
    public class ActiveDecorator<TEntity> : IRepository<TEntity> where TEntity : class, IActive
    {
        private readonly IRepository<TEntity> _repository;

        public ProductContext Db => _repository.Db;

        public ActiveDecorator(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Create(TEntity item)
        {
            item.ActiveStateId = State.Active;
            return _repository.Create(item);
        }

        public void Delete(TEntity item)
        {
            item.ActiveStateId = State.Disable;
        }

        public IQueryable<TEntity> GetAllItems()
        {
            return _repository.GetAllItems()
                .Where(i => i.ActiveStateId == State.Active);
        }

        public TEntity GetById(int id)
        {
            var item = _repository.GetById(id);
            return item?.ActiveStateId == State.Active
                ? item
                : null;
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
