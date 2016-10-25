
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class CompanyDao : ICompanyDao
    {
        private readonly IRepository<Company> _repository;

        public CompanyDao(IRepository<Company> repository)
        {
            _repository = repository;
        }

        public IQueryable<Company> GetAllItems()
        {
           return _repository.GetAllItems();
        }
    }
}
