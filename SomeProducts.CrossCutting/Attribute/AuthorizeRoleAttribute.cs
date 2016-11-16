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

public static class UserRoleHelper
{
    private static readonly IDictionary<UserRole, string> UserRoleDictionary = new Dictionary<UserRole, string>()
    {
        {UserRole.Admin, nameof(UserRole.Admin) },
        {UserRole.User, nameof(UserRole.User) },
        {UserRole.SuperAdmin, nameof(UserRole.SuperAdmin) }
    };
    public static string AsString(this UserRole role)
    {
        return UserRoleDictionary[role];
    }
}

namespace SomeProducts.Attribute
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly UserRole[] _userRoles;

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
            return  _userRoles?.Any(role => httpContext.User.IsInRole(role.AsString())) 
                ?? isAuthorized;
        }
    }
}