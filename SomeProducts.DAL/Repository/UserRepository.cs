
using System.Data.Entity.Migrations;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Context;
using System.Linq;
using System.Threading.Tasks;
using SomeProducts.DAL.Models.ModelState;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductContext _db;

        public UserRepository(ProductContext db)
        {
            _db = db;
        }

        ~UserRepository()
        {
            Dispose();
        }

        public User Create(User user)
        {
            return _db.Users.Add(user);
        }

        public void Delete(User user)
        {
            user.ActiveStateId = State.Disable;
            Update(user);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IQueryable<User> GetAllItems()
        {
            return _db.Users.AsQueryable()
                .Where(u => u.ActiveStateId == State.Active);
        }

        public User GetById(int id)
        {
            var user = _db.Users.Find(id);
            return user.ActiveStateId == State.Active
                ? user
                : null;
        }

        public User GetCompanyItem(int companyId, int itemId)
        {
            return GetCompanyItems(companyId).FirstOrDefault(i => i.Id == itemId);
        }

        public IQueryable<User> GetCompanyItems(int companyId)
        {
            return _db.Users.Where(i => i.CompanyId == companyId);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public bool Update(User user)
        {
            _db.Users.AddOrUpdate(user);
            return true;
        }
    }
}
