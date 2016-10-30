using System.Linq;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface IUserDao : IUserPasswordStore<User, int>,
        IUserRoleStore<User, int>
    {
        int GetUserCount(int? companyId);

        IQueryable<User> GetAllUsers();

        IQueryable<User> GetCompanyUsers(int companyId);
    }
}