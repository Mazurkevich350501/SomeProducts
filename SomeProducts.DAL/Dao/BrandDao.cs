
using System.Collections.Generic;
using System.ComponentModel;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;
using System.Linq;

namespace SomeProducts.DAL.Dao
{
    public class BrandDao : IBrandDao
    {
        private readonly IRepository<Brand> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly IAuditDao _auditDao;
        private readonly IUserHelper _user;

        public BrandDao(
            IRepository<Brand> repository,
            IRepository<Product> productRepository,
            IAuditDao auditDao, 
            IUserHelper user)
        {
            _repository = repository;
            _productRepository = productRepository;
            _auditDao = auditDao;
            _user = user;
        }

        public void CreateBrand(Brand brand)
        {
            if (brand.CompanyId == CrossCutting.Constants.Constants.EmtyCompanyId)
            {
                throw new WarningException("This user can't create product. User company is empty");
            }
            brand = _repository.Create(brand);
            _repository.Save();
            _auditDao.CreateCreateAuditItem(brand);
        }

        public IEnumerable<Brand> GetCompanyBrands(int companyId)
        {
            return _repository.GetCompanyItems(companyId);
        }

        public bool IsBrandUsing(int id)
        {
            return _productRepository.GetCompanyItems(_user.GetCompany())
                .Any(p => p.Brand.Id == id);
        }

        public void RemoveBrand(Brand brand)
        {
            _auditDao.CreateDeleteAuditItem(brand);
            _repository.Delete(brand);
            _repository.Save();
        }
        
        public bool UpdateBrand(Brand brand)
        {
            var previousBrand = _repository.GetById(brand.Id);
            _auditDao.CreateEditAuditItems(previousBrand, brand);
            if (!_repository.Update(brand)) return false;
            _repository.Save();
            return true;
        }

        public Brand GetById( int id)
        {
            var brand = _repository.GetById(id);
            return brand?.CompanyId == _user.GetCompany()
                ? brand
                : null;
        }
    }
}
