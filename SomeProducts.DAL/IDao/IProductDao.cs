using System;
using System.Linq;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface IProductDao
    {
        Product GetProduct(int id);

        Product GetLastProduct(int companyId);

        bool UpdateProduct(Product product);

        void CreateProduct(Product product);

        void RemoveProduct(Product product);
        
        IQueryable<Product> GetAllProducts();

        IQueryable<Product> GetCompanyProducts(int companyId);

        Product GetProduct(int companyId, int productId);

        int GetCompanyProductCount(int? companyId);
    }
}
