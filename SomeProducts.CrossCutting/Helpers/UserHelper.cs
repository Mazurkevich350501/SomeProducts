using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SomeProducts.CrossCutting.Helpers
{
    public static class UserHelper
    {
        public static IPrincipal LastUser { get; private set; }

        public static int GetCompany(this IPrincipal principal)
        {
            LastUser = principal;
            if (!principal.Identity.IsAuthenticated) return -1;
            var claimsIdentity = (ClaimsIdentity) principal.Identity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "CompanyId");
            var result = claim != null ? Convert.ToInt32(claim.Value) : -1;
            return result != 1 ? result : -1;
        }

        public static bool IsInCompany(this IPrincipal principal)
        {
            LastUser = principal;
            return principal.GetCompany() != -1;
        }

        public static int GetUserId(this IPrincipal principal)
        {
            LastUser = principal;
            if (!principal.Identity.IsAuthenticated) return -1;
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Id");
            var result = claim != null ? Convert.ToInt32(claim.Value) : -1;
            return result != 1 ? result : -1;
        }
    }
}