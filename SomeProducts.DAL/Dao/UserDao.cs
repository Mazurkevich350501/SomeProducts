
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Dao
{
    public class UserDao : IUserPasswordStore<User, int>
    {
        private readonly IRepository<User> _repository;

        public UserDao(IRepository<User> repository)
        {
            _repository = repository;
        }

        public Task CreateAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                _repository.Create(user);
                _repository.Save();
            });
        }

        public Task DeleteAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                _repository.Delete(user);
                _repository.Save();
            });
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task.Factory.StartNew(() => _repository.GetAllItems().FirstOrDefault(u => u.Id == userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() => _repository.GetAllItems().FirstOrDefault(u => u.UserName == userName));
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.Factory.StartNew(() => 
                _repository.GetAllItems().FirstOrDefault(u => u.UserName == user.UserName)?.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                return _repository.GetAllItems().FirstOrDefault(u => 
                    u.UserName == user.UserName)?.Password != null;  
            });
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.Factory.StartNew(() =>
            {
                var dbUser = _repository.GetAllItems().FirstOrDefault(u => u.UserName == user.UserName);
                if(dbUser == null) return;
                UpdateAsync(user);
            });
        }

        public Task UpdateAsync(User user)
        {
            return Task.Factory.StartNew(() =>
            {
                _repository.Update(user);
                _repository.Save();
            });
        }
        public static UserDao Create()
        {
            return new UserDao(new UserRepository(new ProductContext()));
        }
    }
}
