
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace SomeProducts.Helpers
{
    public static class CompanyHelper
    {
        public static int GetCompany(this IPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated) return -1;
            var claimsIdentity = (ClaimsIdentity) principal.Identity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "CompanyId");
            return claim != null ? Convert.ToInt32(claim.Value) : -1;
        }
    }
}