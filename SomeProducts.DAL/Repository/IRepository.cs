using System;
using System.Collections.Generic;

namespace SomeProducts.DAL.Repository
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAllItems();
        T GetById(int id);
        void Create(T item);
        bool Update(T item);
        void Delete(int id);
        void Save();
        T GetLast();
        DateTime GetCreateTime(int id);
    }
}