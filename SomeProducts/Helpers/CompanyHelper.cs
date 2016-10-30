
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace SomeProducts.Helpers
{
    public static class CompanyHelper
    {
        public static int GetCompany(this IPrincipal principal)
        {
            return 2; //UserCompanyId(principal.Identity.GetUserId<int>());
        } 
    }
}