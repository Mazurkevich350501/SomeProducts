
namespace SomeProducts.DAL.Models.Audit
{
    public enum Status
    {
        Edit = 1,
        Create,
        Delete
    }

    public class AuditStatus
    {
        public Status Id { get; set; }

        public string Value { get; set; }
    }
}
