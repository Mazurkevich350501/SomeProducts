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
        private readonly BrandRepository _repository = new BrandRepository();
        private readonly ProductRepository _productRepository = new ProductRepository();

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
    }
}
