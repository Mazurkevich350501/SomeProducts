
using System.Threading.Tasks;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IRepositoryAsync<T> 
    {
        Task<T> GetByIdAsync(int id);
        Task SaveAsync();
    }
}
