
using System.Linq;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface ICompanyDao
    {
        IQueryable<Company> GetAllItems();

    }
}
