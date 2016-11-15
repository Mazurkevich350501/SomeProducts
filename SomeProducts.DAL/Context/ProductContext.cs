using System.Data.Entity;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Models.ModelState;

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

        public DbSet<AuditItem> AuditItems { get; set; }

        public DbSet<AuditStatus> AuditStatuses { get; set; }

        public DbSet<AuditEntity> AuditEntities { get; set; }

        public DbSet<ActiveState> ActiveStates { get; set; }
    }
}