using System;
using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Dao
{
    public class BrandDao : IBrandDao
    {
        private readonly IRepository<Brand> _repository;
        private readonly IRepository<Product> _productRepository;

        public BrandDao(IRepository<Brand> repository, IRepository<Product> productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }


        public void CreateBrand(Brand brand)
        {
            _repository.Create(brand);
            _repository.Save();
        }

        public IEnumerable<Brand> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public bool IsBrandUsing(int id)
        {
            return _productRepository.GetAllItems().Any(p => p.BrandId == id);
        }

        public void RemoveBrand(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public DateTime GetCreateTime(int id)
        {
            return _repository.GetCreateTime(id);
        }

        public bool UpdateBrand(Brand brand)
        {
            if (_repository.Update(brand))
            {
                _repository.Save();
                return true;   
            }
            return false;
        }
    }
}
