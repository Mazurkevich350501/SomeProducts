
using System.Data.Entity.Migrations;
using System.Linq;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Repository.Interface;


namespace SomeProducts.DAL.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
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
        ~BaseRepository()
        {
            Dispose();
        }

        public TEntity Create(TEntity item)
        {
            return _db.Set<TEntity>().Add(item);
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

        public void Save()
        {
            _db.SaveChanges();
        }

        public bool Update(TEntity item)
        { 
            _db.Set<TEntity>().AddOrUpdate(item);
            return true;
        }
    }
}