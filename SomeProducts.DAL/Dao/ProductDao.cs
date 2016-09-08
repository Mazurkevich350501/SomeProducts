
using System;
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

        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
            _repository.Save();
        }

        public Product GetProduct(int id)
        {
            return _repository.GetById(id);
        }

        public void RemoveProduct(int id)
        {
            _repository.Delete(id);
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
    }
}
