
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
        private readonly IAuditDao _auditDao;

        public BrandDao(IDateModifiedRepository<Brand> repository, IDateModifiedRepository<Product> productRepository, IAuditDao auditDao)
        {
            _repository = repository;
            _productRepository = productRepository;
            _auditDao = auditDao;
        }

        public void CreateBrand(Brand brand, int userId)
        {
            brand = _repository.Create(brand);
            _repository.Save();
            _auditDao.CreateCreateAuditItem(brand, userId);
        }

        public IEnumerable<Brand> GetCompanyBrands(int companyId)
        {
            return _repository.GetCompanyItems(companyId);
        }

        public bool IsBrandUsing(int companyId ,int id)
        {
            return _productRepository.GetCompanyItems(companyId).Any(p => p.Brand.Id == id);
        }

        public void RemoveBrand(Brand brand, int userId)
        {
            _auditDao.CreateDeleteAuditItem(brand, userId);
            _repository.Delete(brand);
            _repository.Save();
        }
        
        public bool UpdateBrand(Brand brand, int userId)
        {
            var previousBrand = _repository.GetById(brand.Id);
            _auditDao.CreateEditAuditItems(previousBrand, brand, userId);
            if (!_repository.Update(brand)) return false;
            _repository.Save();
            return true;
        }

        public Brand GetById(int companyId, int id)
        {
            var brand = _repository.GetById(id);
            return brand?.CompanyId == companyId
                ? brand
                : null;
        }
    }
}
