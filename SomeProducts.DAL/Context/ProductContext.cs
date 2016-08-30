using System.Data.Entity;
using SomeProducts.DAL.Models;
using SomeProducts.Models.ProductModels;

namespace SomeProducts.DAL.Context
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