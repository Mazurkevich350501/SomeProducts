using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Models;
using SomeProducts.Repository;

namespace SomeProducts.DAL.Repository
{
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

        public IEnumerable<Brand> GetAllItems()
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

        public Brand GetLast()
        {
            return db.Brands.LastOrDefault();
        }
    }

}