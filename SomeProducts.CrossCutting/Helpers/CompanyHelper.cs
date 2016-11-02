using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SomeProducts.CrossCutting.Helpers
{
    public static class CompanyHelper
    {
        public static int GetCompany(this IPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated) return -1;
            var claimsIdentity = (ClaimsIdentity) principal.Identity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "CompanyId");
            var result = claim != null ? Convert.ToInt32(claim.Value) : -1;
            return result != 1 ? result : -1;
        }

        public static bool IsInCompany(this IPrincipal principal)
        {
            return principal.GetCompany() != -1;
        }
    }
}