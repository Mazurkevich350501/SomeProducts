using SomeProducts.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SomeProducts.DbRepository
{
    interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetList();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }

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

        public IEnumerable<Product> GetList()
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

    public class BrandRepository : IRepository<Brand>
    {
        private ProductContext db;

        public BrandRepository()
        {
            this.db = new ProductContext();
        }

        public void Create(Brand item)
        {
            db.Brands.Add(item);
        }

        public void Delete(int id)
        {
            Brand brand = db.Brands.Find(id);
            if (brand != null)
                db.Brands.Remove(brand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Brand GetById(int id)
        {
            return db.Brands.Find(id);
        }

        public IEnumerable<Brand> GetList()
        {
            return db.Brands;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Brand item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}