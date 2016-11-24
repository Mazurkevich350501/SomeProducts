
using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Models.ModelState;
using SomeProducts.PresentationServices.IPresentationSevices.Admin;
using SomeProducts.PresentationServices.Models.Admin;

namespace SomeProducts.PresentationServices.PresentationServices.Admin
{
    public class CompanyPresentationService : ICompanyPresentationService
    {
        private readonly ICompanyDao _dao;
        private readonly IUserDao _userDao;

        public CompanyPresentationService(ICompanyDao dao, IUserDao userDao)
        {
            _dao = dao;
            _userDao = userDao;
        }

        public CompanyModel CreteNewCompany(string companyName)
        {
            var company = new Company()
            {
                CompanyName = companyName,
                ActiveStateId = State.Active
            };
            _dao.CreateCompany(company);
            return CompanyModelCast(company);
        }

        public CompaniesViewModel GetCompaniesViewModel()
        {
            var companies = _dao.GetAllItems()
                .Where(c => c.Id != CrossCutting.Constants.Constants.EmtyCompanyId).ToList()
                .Select(CompanyModelCast)
                .OrderBy(c => c.CompanyName).ToList();
            return new CompaniesViewModel()
            {
                Companies = companies
            };
        }

        public void RemoveCompany(int id)
        {
            var company = _dao.GetCompanyById(id);
            if (company == null)
                return;
            foreach (var user in company.Users)
            {
                user.CompanyId = CrossCutting.Constants.Constants.EmtyCompanyId;
                _userDao.RemoveFromRoleAsync(user, UserRole.Admin.AsString());
            }
            _dao.RemoveCompany(company);
        }

        public bool UpdateCompany(CompanyModel model)
        {
            return _dao.UpdateCompany(CompanyCast(model));
        }

        private static Company CompanyCast(CompanyModel model)
        {
            return new Company()
            {
                ActiveStateId = State.Active,
                Id = model.CompanyId,
                CompanyName = model.CompanyName
            };
        }

        private static CompanyModel CompanyModelCast(Company company)
        {
            return new CompanyModel()
            {
                CompanyId = company.Id,
                CompanyName = company.CompanyName,
                CompanyUsers = CreateCompanyUserCollection(company)
            };
        }

        private static ICollection<CompanyUser> CreateCompanyUserCollection(Company company)
        {
            return company?.Users?.Select(companyUser => new CompanyUser()
            {
                Name = companyUser.UserName,
                MainRole = GetMainRole(companyUser)
            }).OrderBy(u => u.MainRole).ThenBy(u => u.Name).ToList();
        }

        private static string GetMainRole(User user)
        {
            if (user.Roles.Any(r => r.Name == UserRole.SuperAdmin.AsString()))
            {
                return UserRole.SuperAdmin.AsString();
            }
            if (user.Roles.Any(r => r.Name == UserRole.Admin.AsString()))
            {
                return UserRole.Admin.AsString();
            }
            return UserRole.User.AsString();
        }
    }
}
