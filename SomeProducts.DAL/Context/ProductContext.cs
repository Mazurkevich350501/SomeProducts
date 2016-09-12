using System.Data.Entity;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(string baseString) : base(baseString)
        {
        }

        public DbSet<Product> Products { set; get; }
        public DbSet<Brand> Brands { set; get; }
    }
}