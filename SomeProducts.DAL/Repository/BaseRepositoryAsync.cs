
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Repository
{
    public class BaseRepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity: class
    {
        private readonly ProductContext _db;

        public BaseRepositoryAsync(ProductContext db)
        {
            _db = db;
        }

        ~BaseRepositoryAsync()
        {
            Dispose();
        }

        public void Create(TEntity item)
        {
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

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public bool Update(TEntity item)
        {
            _db.Set<TEntity>().AddOrUpdate(item);
            return true;
        }
    }
}