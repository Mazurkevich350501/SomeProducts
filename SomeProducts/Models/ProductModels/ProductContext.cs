using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SomeProducts.Models.ProductModels
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("DefaultConnection")
        {
        }
        public DbSet<Product> Products { set; get; }
        public DbSet<Brand> Brands { set; get; }
    }
}