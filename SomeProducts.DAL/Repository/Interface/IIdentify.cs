
namespace SomeProducts.DAL.Repository.Interface
{
    public interface IIdentify
    {
        int Id { get; set; }

        byte[] RowVersion { get; set; }
    }
}
