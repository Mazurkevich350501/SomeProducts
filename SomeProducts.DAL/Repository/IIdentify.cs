
namespace SomeProducts.DAL.Repository
{
    public interface IIdentify
    {
        int Id { get; set; }

        byte[] RowVersion { get; set; }
    }
}
