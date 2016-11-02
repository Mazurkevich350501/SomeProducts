using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SomeProducts.CrossCutting.Helpers;

public enum UserRole
{
    Admin = 1,
    User,
    SuperAdmin
}

namespace SomeProducts.Attribute
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly UserRole[] _userRoles;

        private static readonly Dictionary<UserRole, string> RolesDictionary = new Dictionary<UserRole, string>()
        {
            {UserRole.SuperAdmin, nameof(UserRole.SuperAdmin) },
            {UserRole.Admin, nameof(UserRole.Admin) },
            {UserRole.User, nameof(UserRole.User) }
        };

        public AuthorizeRoleAttribute()
        {
            _userRoles = null;
        }

        public AuthorizeRoleAttribute(params UserRole[] userRoles)
        {
            _userRoles = userRoles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext.User.GetCompany() == CrossCutting.Constants.Constants.EmtyCompanyId)
                return false;
            var isAuthorized = base.AuthorizeCore(httpContext);
            return  _userRoles?.Any(role => httpContext.User.IsInRole(RolesDictionary[role])) 
                ?? isAuthorized;
        }
    }
}