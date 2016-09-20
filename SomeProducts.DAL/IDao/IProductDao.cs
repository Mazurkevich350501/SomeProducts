using System;
using System.Collections.Generic;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface IProductDao
    {
        Product GetProduct(int id);

        Product GetLastProduct();

        bool UpdateProduct(Product product);

        void CreateProduct(Product product);

        void RemoveProduct(int id);

        DateTime GetCreateTime(int id);

        ICollection<Product> GetAllProducts();
    }
}
