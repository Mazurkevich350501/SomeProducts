using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SomeProducts.Helpers
{
    public static class PrincipalHelper
    {
        public static int GetCompany(this IPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated) return -1;
            var claimsIdentity = (ClaimsIdentity) principal.Identity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "CompanyId");
            var result = claim != null ? Convert.ToInt32(claim.Value) : -1;
            return result != 1 ? result : -1;
        }

        public static bool IsInCompany(this IPrincipal principal, int? companyId)
        {
            return companyId == null
                ? principal.GetCompany() != -1
                : principal.GetCompany() == companyId.Value;
        }

        public static int? GetSuperAdminCompany(this IPrincipal principal)
        {
            if (principal.IsInRole(UserRole.SuperAdmin.AsString())) return null;
            return principal.GetCompany();
        }

        public static int GetUserId(this IPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated) return -1;
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Id");
            var result = claim != null ? Convert.ToInt32(claim.Value) : -1;
            return result != 1 ? result : -1;
        }

        public static bool IsInUserRole(this IPrincipal principal, UserRole role)
        {
            return principal.IsInRole(role.AsString());
        }
    }
}