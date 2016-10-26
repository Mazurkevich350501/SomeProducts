

using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models;

namespace SomeProducts.PresentationServices.Authorize
{
    public class UserValidatorLocal : UserValidator<User, int>
    {
        public UserValidatorLocal(UserManager<User, int> manager) : base(manager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(User item)
        {
            return await Task.FromResult(await base.ValidateAsync(item) == IdentityResult.Success
                ? IdentityResult.Success
                : IdentityResult.Failed($"{item.UserName} {Resources.Resource.UserNameExist}\n"));
        }
    }
}
