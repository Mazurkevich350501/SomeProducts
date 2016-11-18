
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Repository.Decorators
{
    public class AsyncDecorator<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;

        public ProductContext Db => _repository.Db;

        public AsyncDecorator(IRepository<TEntity> repository)
        {
            _repository= repository;
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

        public void Save()
        {
            _repository.Save();
        }

        public async Task SaveAsync()
        {
            await Db.SaveChangesAsync();
        }

        public bool Update(TEntity item)
        {
            return _repository.Update(item);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Db.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetCompanyItems(int companyId)
        {
            return _repository.GetCompanyItems(companyId);
        }

        public TEntity GetCompanyItem(int companyId, int itemId)
        {
            return _repository.GetCompanyItem(companyId, itemId);
        }

        public TEntity GetLast(int companyId)
        {
            return _repository.GetLast(companyId);
        }
    }
}
