using SomeProducts.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SomeProducts.Repository
{
    interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAllItems();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}