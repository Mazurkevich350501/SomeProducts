

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

        public async override Task<IdentityResult> ValidateAsync(User item)
        {
            return await Task.FromResult(await base.ValidateAsync(item) == IdentityResult.Success
                ? IdentityResult.Success
                : IdentityResult.Failed($"{Resources.Resource.IncorrectUserName} {item.UserName}"));
        }
    }
}
