using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.Authorize;
using SomeProducts.PresentationServices.Models.Account;

namespace SomeProducts.PresentationServices.PresentationServices
{
    public class UserPresentationService
    {
        private readonly AccountManager _manager;

        public UserPresentationService(AccountManager manager)
        {
            _manager = manager;
        }

        public async Task<bool> LogIn(LogInUserModel model, IAuthenticationManager authentication)
        {
            var user = await _manager.FindAsync(model.Name, model.Password);
            if (user == null) return false;

            authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            return true;
        }
        
        public Task<IdentityResult> PasswordValidateAsync(string password)
        {
            return _manager.PasswordValidator.ValidateAsync(password);
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
