
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class UserDao : IUserDao
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IAuditDao _auditDao;

        public UserDao(
            IRepository<User> userRepository,
            IRepository<Role> roleRepository,
            IAuditDao auditDao)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _auditDao = auditDao;
        }

        public async Task CreateAsync(User user)
        {
            user.Roles = new List<Role>()
            {
                new Role() {Name = UserRole.User.AsString()}
            };
            _userRepository.Create(user);
            await _userRepository.SaveAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _userRepository.Delete(user);
            _auditDao.CreateDeleteAuditItem(user);
            await _userRepository.SaveAsync();
        }

        public void Dispose()
        {
            _userRepository.Db.Dispose();
            _roleRepository.Db.Dispose();
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await GetAllUsers().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await GetAllUsers().FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<string> GetPasswordHashAsync(User user)
        {
            var tempUser = await GetAllUsers()
                .FirstOrDefaultAsync(u => u.UserName == user.UserName);
            return tempUser?.Password;
        }

        public async Task<bool> HasPasswordAsync(User user)
        {
            var password = await GetPasswordHashAsync(user);
            return password != null;
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash)
        {
            var dbUser = GetAllUsers().FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if(dbUser == null) return;
            await UpdateAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            var previousUser = await FindByIdAsync(user.Id);
            _userRepository.Update(user);
            await _userRepository.SaveAsync();
            _auditDao.CreateEditAuditItems(previousUser, user);
        }

        public async Task AddToRoleAsync(User user, string roleName)
        {
            var role = await _roleRepository.GetAllItems().FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null) return;
            if (user.Roles == null) user.Roles = new List<Role>();
            user.Roles.Add(role);
            await UpdateAsync(user);
        }

        public async Task RemoveFromRoleAsync(User user, string roleName)
        {
            var role = await _roleRepository.GetAllItems().FirstOrDefaultAsync(r => r.Name == roleName);
            if (role != null)
            {
                user.Roles.Remove(role);
                await UpdateAsync(user);
            }
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.FromResult(
                (IList<string>)user.Roles?.Select(role => role.Name).ToList() 
                ?? new List<string>()
                );
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.FromResult(user.Roles.Any(r => r.Name == roleName ));
        }

        public int GetUserCount(int? companyId)
        {
            var users = companyId == null
                ? GetAllUsers()
                : GetCompanyUsers(companyId.Value);
            return users.Count();
        }

        public IQueryable<User> GetAllUsers()
        {
            return _userRepository.GetAllItems();
        }

        public IQueryable<User> GetCompanyUsers(int companyId)
        {
            return _userRepository.GetCompanyItems(companyId);
        }
    }
}
