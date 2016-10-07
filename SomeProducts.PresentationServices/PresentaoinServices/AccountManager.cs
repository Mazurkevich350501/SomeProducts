
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;

namespace SomeProducts.PresentationServices.PresentaoinServices
{
    public class AccountManager : UserManager<User, int>
    {
        public AccountManager(IUserStore<User, int> store) : base(store)
        {
            /*PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };*/
        }
        public override Task<User> FindAsync(string userName, string password)
        {
            return Task<User>.Factory.StartNew(() =>
            {
                var result = PasswordHasher.VerifyHashedPassword(userName, password);
                return result == PasswordVerificationResult.SuccessRehashNeeded
                ? Store.FindByNameAsync(userName).Result
                : null;
            });
        }
    }
}
