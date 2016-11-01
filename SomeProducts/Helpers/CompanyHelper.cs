
using System.Security.Principal;

namespace SomeProducts.Helpers
{
    public static class CompanyHelper
    {
        public static int GetCompany(this IPrincipal principal)
        {
            return 2; //principal.Identity.
        } 
    }
}