
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;

namespace SomeProducts.PresentationServices.Authorize
{
    public class AccountManager : UserManager<User, int>
    {
        public AccountManager(IUserStore<User, int> store) : base(store)
        {
            UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true
            };

            PasswordHasher = new AccountPasswordHasher();
        }

        public override Task<User> FindAsync(string userName, string password)
        {
            return Task<User>.Factory.StartNew(() =>
            {
                var result = Store.FindByNameAsync(userName).Result;
                return PasswordHasher.VerifyHashedPassword(result?.Password, password) == PasswordVerificationResult.SuccessRehashNeeded
                ? result
                : null;
            });
        }
    }
}