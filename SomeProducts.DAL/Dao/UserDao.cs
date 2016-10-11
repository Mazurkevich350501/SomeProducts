
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class UserDao : IUserPasswordStore<User, int> , IUserRoleStore<User, int>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public UserDao(IRepository<User> userRepository, IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public Task CreateAsync(User user)
        {
            return Task.Factory.StartNew(async () =>
            {
                _userRepository.Create(user);
                await AddToRoleAsync(user, "user");
                _userRepository.Save();
            });
        }

        public Task DeleteAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                _userRepository.Delete(user);
                _userRepository.Save();
            });
        }

        public void Dispose()
        {
            _userRepository.Dispose();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task.Factory.StartNew(() => _userRepository.GetAllItems().FirstOrDefault(u => u.Id == userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() => _userRepository.GetAllItems().FirstOrDefault(u => u.UserName == userName));
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.Factory.StartNew(() => 
                _userRepository.GetAllItems().FirstOrDefault(u => u.UserName == user.UserName)?.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                return _userRepository.GetAllItems().FirstOrDefault(u => 
                    u.UserName == user.UserName)?.Password != null;  
            });
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.Factory.StartNew(() =>
            {
                var dbUser = _userRepository.GetAllItems().FirstOrDefault(u => u.UserName == user.UserName);
                if(dbUser == null) return;
                UpdateAsync(user);
            });
        }

        public Task UpdateAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                _userRepository.Update(user);
                _userRepository.Save();
            });
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            return Task.Factory.StartNew(() =>
            {
                var role = _roleRepository.GetAllItems().FirstOrDefault(r => r.Name == roleName);
                if (role == null) return;
                if (user.Roles == null) user.Roles = new List<Role>();
                user.Roles.Add(role);
                _roleRepository.Save();
            });
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            return Task.Factory.StartNew(() =>
            {
                var role = _roleRepository.GetAllItems().FirstOrDefault(r => r.Name == roleName);
                if (role != null)
                {
                    user.Roles.Remove(role);
                }
                _roleRepository.Save();
            });
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                return (IList<string>)user.Roles.Select(role => role.Name).ToList();
            });
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.Factory.StartNew(() => user.Roles.Any(r => r.Name == roleName ));
        }
    }
}
