using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeProducts.DAL.Repository
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> GetAllItems();
        T GetById(int id);
        void Create(T item);
        bool Update(T item);
        void Delete(int id);
        void Save();
        T GetLast();
        DateTime GetCreateTime(int id);
    }
}