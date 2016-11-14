using System.Data.Entity;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(string baseString = "DefaultConnection") : base(baseString)
        {
        }
        
        public DbSet<Product> Products { set; get; }

        public DbSet<Brand> Brands { set; get; }
        
        public DbSet<User> Users { set; get; }
        
        public DbSet<Role> Roles { get; set; }

        public DbSet<Company> Companies { get; set; }
    }
}