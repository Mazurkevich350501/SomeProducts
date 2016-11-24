
using System.Linq;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface ICompanyDao
    {
        IQueryable<Company> GetAllItems();

        Company GetCompanyById(int id);

        Company CreateCompany(Company company);

        void RemoveCompany(Company company);

        bool UpdateCompany(Company company);
    }
}
