
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class RoleDao : IRoleStore<Role, int>
    {
        private readonly IRepository<Role> _repository;

        public RoleDao(IRepository<Role> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Role role)
        {
            _repository.Create(role);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            _repository.Delete(role);
            await _repository.SaveAsync();
        }

        public void Dispose()
        {
            _repository.Db.Dispose();
        }

        public async Task<Role> FindByIdAsync(int roleId)
        {
            return await _repository.GetByIdAsync(roleId);
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            return await _repository.GetAllItems().FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task UpdateAsync(Role role)
        {
            _repository.Update(role);
            await _repository.SaveAsync();
        }
    }
}
