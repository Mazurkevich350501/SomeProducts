using System;
using System.Linq;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> GetAllItems();
        T GetById(int id);
        T Create(T item);
        bool Update(T item);
        void Delete(T item);
        void Save();
    }
}