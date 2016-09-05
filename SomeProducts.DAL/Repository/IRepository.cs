using System;
using System.Collections.Generic;

namespace SomeProducts.DAL.Repository
{
    internal interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAllItems();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
        T GetLast();
        DateTime GetCreateTime(int id);
    }
}