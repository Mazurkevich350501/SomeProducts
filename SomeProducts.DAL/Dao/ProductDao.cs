
using System.ComponentModel;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class ProductDao : IProductDao
    {
        private readonly IDateModifiedRepository<Product> _repository;
        private readonly IDateModifiedRepository<Brand> _brandRepository;

        public ProductDao(IDateModifiedRepository<Product> repository, IDateModifiedRepository<Brand> brandRepository)
        {
            _repository = repository;
            _brandRepository = brandRepository;
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
            IsBrandInCompany(product);
            _repository.Create(product);
            _repository.Save();
        }

        public Product GetLastProduct(int companyId)
        {
            return _repository.GetLast(companyId);
        }
       
        public IQueryable<Product> GetAllProducts()
        {
            return _repository.GetAllItems();
        }
        
        public IQueryable<Product> GetCompanyProducts(int companyId)
        {
            return _repository.GetCompanyItems(companyId);
        }

        public Product GetProduct(int companyId, int productId)
        {
            return _repository.GetCompanyItem(companyId, productId);
        }

        private void IsBrandInCompany(Product product)
        {
            var brand = _brandRepository.GetById(product.BrandId);
            if (product.CompanyId == brand.CompanyId)
            {
                return;
            }
            throw new WarningException("Brand.CompanyId does not coincide with Product.CompanyId");
        }

        public int GetCompanyProductCount(int? companyId)
        {
            return companyId == null
                ? GetAllProducts().Count()
                : GetCompanyProducts(companyId.Value).Count();
        }
    }
}
