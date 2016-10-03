
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.Models.Account;

namespace SomeProducts.PresentationServices.PresentaoinServices
{
    public class UserPresentationService
    {
        private readonly AccountManager _manager;

        public UserPresentationService(AccountManager manager)
        {
            _manager = manager;
        }

        public Task<User> LogInAsync(LogInUserModel model)
        {
            return _manager.FindAsync(model.Name, model.Password);
        }

        public Task<IdentityResult> CreateAsync(RegistrationViewModel model)
        {
            return _manager.CreateAsync(UserCast(model));
        }

        private static User UserCast(RegistrationViewModel model)
        {
            if (model.ConfirmPassword != model.Password) return null;
            return new User()
            {
                UserName = model.Name,
                Password = model.Password
            };
        }
    }
}
