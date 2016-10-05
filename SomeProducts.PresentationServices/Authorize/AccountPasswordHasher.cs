
using Microsoft.AspNet.Identity;

namespace SomeProducts.PresentationServices.Authorize
{
    public class AccountPasswordHasher : PasswordHasher
    {
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return hashedPassword == providedPassword 
                ? PasswordVerificationResult.SuccessRehashNeeded 
                : PasswordVerificationResult.Failed;
        }
    }
}
