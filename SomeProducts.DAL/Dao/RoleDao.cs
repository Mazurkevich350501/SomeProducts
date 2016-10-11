
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class RoleDao : IRoleStore<Role, int>
    {
        private readonly IRepository<Role> _repository;

        public RoleDao(RoleRepository repository)
        {
            _repository = repository;
        }

        public Task CreateAsync(Role role)
        {
            return Task.Factory.StartNew(() =>
            {
                _repository.Create(role);
                _repository.Save();
            });
        }

        public Task DeleteAsync(Role role)
        {
            return Task.Factory.StartNew(() =>
            {
                _repository.Delete(role);
                _repository.Save();
            });
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public Task<Role> FindByIdAsync(int roleId)
        {
            return Task.Factory.StartNew(() => _repository.GetById(roleId));
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Task.Factory.StartNew(() => 
                _repository.GetAllItems().FirstOrDefault(r => r.Name == roleName));
        }

        public Task UpdateAsync(Role role)
        {
            return Task.Factory.StartNew(() =>
            {
                _repository.Update(role);
                _repository.Save();
            });
        }
    }
}
