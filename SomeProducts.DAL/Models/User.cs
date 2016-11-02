using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Models
{
    public class User : IUser<int>, IAvailableCompany
    {
        public User()
        {
            CompanyId = 1;
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [Required]
        public int CompanyId { get; set; }
        
        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("CompanyId", CompanyId.ToString()));
            return userIdentity;
        }
    }
}
