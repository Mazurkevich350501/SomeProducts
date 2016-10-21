
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.Models.Account;

namespace SomeProducts.PresentationServices.Authorize
{
    public class AccountManager : UserManager<User, int>
    {
        public AccountManager(IUserStore<User, int> store) : base(store)
        {
            UserValidator = new UserValidatorLocal(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            PasswordValidator = new PasswordValidatorLocal()
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true
            };

            PasswordHasher = new AccountPasswordHasher();
        }

        public override async Task<User> FindAsync(string userName, string password)
        {
            var result = await Store.FindByNameAsync(userName);
            return PasswordHasher.VerifyHashedPassword(result?.Password, password) == PasswordVerificationResult.SuccessRehashNeeded
            ? result
            : null;
        }

        public override Task<IdentityResult> CreateAsync(User user)
        {
            user.Password = PasswordHasher.HashPassword(user.Password);
            return base.CreateAsync(user);
        }

        public static User UserCast(RegistrationViewModel model)
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