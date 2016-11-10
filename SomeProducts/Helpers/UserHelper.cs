
using System.Security.Principal;
using System.Web;
using SomeProducts.CrossCutting.Helpers;

namespace SomeProducts.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IPrincipal _principal;

        public UserHelper()
        {
            _principal = HttpContext.Current.User;
        }

        public int GetCompany()
        {
            return _principal.GetCompany();
        }

        public int? GetSuperAdminCompany()
        {
            return _principal.GetSuperAdminCompany();
        }

        public int GetUserId()
        {
            return _principal.GetUserId();
        }

        public bool IsInCompany(int? companyId)
        {
            return _principal.IsInCompany(companyId);
        }

        public bool IsInRole(UserRole role)
        {
            return _principal.IsInUserRole(role);
        }
    }
}