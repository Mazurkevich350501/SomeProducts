using System;
using System.Collections.Generic;
using System.Data.Entity;
using SomeProducts.DAL.Context;
using SomeProducts.Models.ProductModels;
using SomeProducts.Repository;

namespace SomeProducts.DAL.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private ProductContext db;

        public ProductRepository()
        {
            this.db = new ProductContext();
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
                db.Products.Remove(product);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Product GetById(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> GetAllItems()
        {
            return db.Products;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}