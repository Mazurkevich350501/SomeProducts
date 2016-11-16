
namespace SomeProducts.DAL.Models.Audit
{
    public enum Entity
    {
        Brand = 1,
        Product,
        User
    }

    public class AuditEntity
    {
        public Entity Id { get; set; }

        public string Name { get; set; }
    }
}
