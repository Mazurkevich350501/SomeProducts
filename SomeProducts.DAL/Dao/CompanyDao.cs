
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class CompanyDao : ICompanyDao
    {
        private readonly IRepository<Company> _repository;
        private readonly IAuditDao _auditDao;

        public CompanyDao(
            IRepository<Company> repository,
            IAuditDao auditDao)
        {
            _repository = repository;
            _auditDao = auditDao;
        }

        public IQueryable<Company> GetAllItems()
        {
           return _repository.GetAllItems();
        }

        public Company GetCompanyById(int id)
        {
            return _repository.GetById(id);
        }

        public Company CreateCompany(Company company)
        {
            var result = _repository.Create(company);
            _repository.Save();
            _auditDao.CreateCreateAuditItem(result);
            return result;
        }

        public void RemoveCompany(Company company)
        {
            _auditDao.CreateDeleteAuditItem(company);
            _repository.Delete(company);
            _repository.Save();
        }

        public bool UpdateCompany(Company company)
        {
            var previousValue = _repository.GetById(company.Id);
            _auditDao.CreateEditAuditItems(previousValue, company);
            var result = _repository.Update(company);
            _repository.Save();
            return result;
        }
    }
}
