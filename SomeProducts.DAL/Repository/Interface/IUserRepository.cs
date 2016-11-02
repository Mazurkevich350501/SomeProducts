using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IUserRepository : IRepositoryAsync<User>, ICompanyItemsAccess<User>
    {
    }
}