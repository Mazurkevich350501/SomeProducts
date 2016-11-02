using System.Linq;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface ICompanyItemsAccess<out T>
    {
        IQueryable<T> GetCompanyItems(int companyId);
        T GetCompanyItem(int companyId, int itemId);
    }
}