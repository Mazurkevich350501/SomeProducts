using SomeProducts.PresentationServices.Models.Admin;

namespace SomeProducts.PresentationServices.IPresentationSevices.Admin
{
    public interface ICompanyPresentationService
    {
        CompaniesViewModel GetCompaniesViewModel();

        void RemoveCompany(int id);

        bool UpdateCompany(CompanyModel model);

        CompanyModel CreteNewCompany(string companyName);
    }
}
