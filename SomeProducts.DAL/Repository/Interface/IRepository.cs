using System;
using System.Linq;
using SomeProducts.DAL.Context;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IRepository<T>: 
        IRepositoryAsync<T>,
        ICompanyItemsAccess<T>, 
        IDateModifiedRepository<T>
    {
        ProductContext Db { get; }
        IQueryable<T> GetAllItems();
        T GetById(int id);
        T Create(T item);
        bool Update(T item);
        void Delete(T item);
        void Save();
    }
}