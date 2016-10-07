
using System;
using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Dao
{
    public class ProductDao : IProductDao
    {
        private readonly IRepository<Product> _repository;

        public ProductDao(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public bool UpdateProduct(Product product)
        {
            var result = _repository.Update(product);
            _repository.Save();
            return result;
        }

        public Product GetProduct(int id)
        {
            return _repository.GetById(id);
        }

        public void RemoveProduct(Product product)
        {
            _repository.Delete(product);
            _repository.Save();
        }

        public void CreateProduct(Product product)
        {
            _repository.Create(product);
            _repository.Save();
        }

        public Product GetLastProduct()
        {
            return _repository.GetLast();
        }

        public DateTime GetCreateTime(int id)
        {
            return _repository.GetCreateTime(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAllItems();
        }

        public int GetProductCount()
        {
            return GetAllProducts().Count();
        }
    }
}
