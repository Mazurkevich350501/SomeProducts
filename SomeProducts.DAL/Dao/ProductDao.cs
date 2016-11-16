
using System.ComponentModel;
using System.Linq;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class ProductDao : IProductDao
    {
        private readonly IDateModifiedRepository<Product> _repository;
        private readonly IDateModifiedRepository<Brand> _brandRepository;
        private readonly IAuditDao _auditDao;
        private readonly IUserHelper _user;

        public ProductDao(IDateModifiedRepository<Product> repository,
            IDateModifiedRepository<Brand> brandRepository,
            IAuditDao auditDao, 
            IUserHelper user)
        {
            _repository = repository;
            _brandRepository = brandRepository;
            _auditDao = auditDao;
            _user = user;
        }

        public bool UpdateProduct(Product product)
        {
            CompanyVerify(product);
            var previousProduct = _repository.GetById(product.Id);
            _auditDao.CreateEditAuditItems(previousProduct, product);
            var result = _repository.Update(product);
            _repository.Save();
            return result;
        }

        public Product GetProduct(int id)
        {
            return _user.IsInRole(UserRole.SuperAdmin)
                ? _repository.GetById(id)
                : null;
        }

        public void RemoveProduct(Product product)
        {
            _auditDao.CreateDeleteAuditItem(product);
            _repository.Delete(product);
            _repository.Save();
        }

        public void CreateProduct(Product product)
        {
            if (product.CompanyId == CrossCutting.Constants.Constants.EmtyCompanyId)
            {
                throw new WarningException("This user can't create product. User company is empty");
            }
            CompanyVerify(product);
            product = _repository.Create(product);
            _repository.Save();
            _auditDao.CreateCreateAuditItem(product);
        }

        public Product GetLastProduct()
        {
            return _repository.GetLast(_user.GetCompany());
        }
       
        public IQueryable<Product> GetAllProducts()
        {
            return _user.IsInRole(UserRole.SuperAdmin)
                ? _repository.GetAllItems()
                : null;
        }
        
        public IQueryable<Product> GetCompanyProducts(int? companyId)
        {
            return companyId != null
                ? _repository.GetCompanyItems(companyId.Value)
                : _repository.GetAllItems();
        }

        public Product GetProduct(int? companyId, int productId)
        {
            return companyId != null
                ? _repository.GetCompanyItem(companyId.Value, productId)
                : _repository.GetById(productId);
        }

        private void CompanyVerify(Product product)
        {
            var brand = _brandRepository.GetById(product.BrandId);
            if (product.CompanyId != brand.CompanyId)
            {
                throw new WarningException("Brand.CompanyId does not coincide with Product.CompanyId");
            }
        }

        public int GetProductCount(int? companyId)
        {
            return companyId == null
                ? GetAllProducts().Count()
                : GetCompanyProducts(companyId.Value).Count();
        }
    }
}
