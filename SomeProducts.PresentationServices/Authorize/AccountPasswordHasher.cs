

using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Identity;

namespace SomeProducts.PresentationServices.Authorize
{
    public class AccountPasswordHasher : PasswordHasher
    {
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var encodePassword = HashPassword(providedPassword);
            return hashedPassword == encodePassword
                ? PasswordVerificationResult.SuccessRehashNeeded 
                : PasswordVerificationResult.Failed;
        }

        public override string HashPassword(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var bytePassword = Encoding.UTF8.GetBytes(password + CrossCutting.Constants.Constants.Salt);
            var hash = md5.ComputeHash(bytePassword);
            return Encoding.UTF8.GetString(hash);
        }
    }
}
