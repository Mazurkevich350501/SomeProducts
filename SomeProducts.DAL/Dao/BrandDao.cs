
using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class BrandDao : IBrandDao
    {
        private readonly IDateModifiedRepository<Brand> _repository;
        private readonly IDateModifiedRepository<Product> _productRepository;

        public BrandDao(IDateModifiedRepository<Brand> repository, IDateModifiedRepository<Product> productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public void CreateBrand(Brand brand)
        {
            _repository.Create(brand);
            _repository.Save();
        }

        public IEnumerable<Brand> GetCompanyBrands(int companyId)
        {
            return _repository.GetCompanyItems(companyId);
        }

        public bool IsBrandUsing(int companyId ,int id)
        {
            return _productRepository.GetCompanyItems(companyId).Any(p => p.Brand.Id == id);
        }

        public void RemoveBrand(Brand brand)
        {
            _repository.Delete(brand);
            _repository.Save();
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

        public Brand GetById(int companyId, int id)
        {
            var brand = _repository.GetById(id);
            return brand.CompanyId == companyId
                ? brand
                : null;
        }
    }
}
