
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IRepositoryAsync<T> : IDisposable
    {
        IQueryable<T> GetAllItems();
        T GetById(int id);
        T Create(T item);
        bool Update(T item);
        void Delete(T item);
        Task SaveAsync();
    }
}
