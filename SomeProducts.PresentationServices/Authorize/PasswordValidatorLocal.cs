
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using R = Resources.LocalResource;

namespace SomeProducts.PresentationServices.Authorize
{
    public class PasswordValidatorLocal : PasswordValidator
    {
        public override Task<IdentityResult> ValidateAsync(string item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(item) || item.Length < RequiredLength)
            {
                errors.Add($"{R.PasswordRequireLength}: {RequiredLength}.");
            }
            if (RequireNonLetterOrDigit && item.All(IsLetterOrDigit))
            {
                errors.Add(R.PasswordRequireNonLetterOrDigit);
            }
            if (RequireDigit && item.All(c => !IsDigit(c)))
            {
                errors.Add(R.PasswordRequireDigit);
            }
            if (RequireLowercase && item.All(c => !IsLower(c) && !IsRuLower(c)))
            {
                errors.Add(R.PasswordRequireLower);
            }
            if (RequireUppercase && item.All(c => !IsUpper(c) && !IsRuUpper(c)))
            {
                errors.Add(R.PasswordRequireUpper);
            }
            return Task.FromResult(errors.Count == 0 
                ? IdentityResult.Success 
                : IdentityResult.Failed(string.Join(" ", errors)));
        }

        private static bool IsRuLower(char c)
        {
            return c > 'а' && c < 'я';
        }

        private static bool IsRuUpper(char c)
        {
            return c > 'А' && c < 'Я';
        }
    }
}
