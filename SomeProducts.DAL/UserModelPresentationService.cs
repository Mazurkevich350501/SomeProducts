using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL
{
    public class UserModelPresentationService : UserManager<User>
    {
        public UserModelPresentationService(IUserStore<User> store) : base(store)
        {
            this.PasswordHasher = new UserPasswordHasher();
        }
        public override Task<User> FindAsync(string userName, string password)
        {
            var taskInvoke = Task<User>.Factory.StartNew(() =>
            {
                var result = PasswordHasher.VerifyHashedPassword(userName, password);
                return result == PasswordVerificationResult.SuccessRehashNeeded 
                    ? Store.FindByNameAsync(userName).Result 
                    : null;
            });
            return taskInvoke;
        }
    }

    public class UserPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return hashedPassword == providedPassword 
                ? PasswordVerificationResult.SuccessRehashNeeded 
                : PasswordVerificationResult.Failed;
        }
    }
}
