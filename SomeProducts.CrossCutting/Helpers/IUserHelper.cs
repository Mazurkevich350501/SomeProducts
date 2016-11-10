
namespace SomeProducts.CrossCutting.Helpers
{
    public interface IUserHelper
    {
        int GetCompany();

        bool IsInCompany(int? companyId);

        int? GetSuperAdminCompany();

        int GetUserId();

        bool IsInRole(UserRole role);
    }
}
