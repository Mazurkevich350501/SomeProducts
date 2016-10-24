using System;
using System.Linq;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface IProductDao
    {
        Product GetProduct(int id);

        Product GetLastProduct();

        bool UpdateProduct(Product product);

        void CreateProduct(Product product);

        void RemoveProduct(Product product);

        DateTime GetCreateTime(int id);

        int GetProductCount();

        IQueryable<Product> GetAllProducts();
    }
}
