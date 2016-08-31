using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SomeProducts.Repository
{
    internal interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAllItems();
        T GetById(int id);
        T GetLast();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}