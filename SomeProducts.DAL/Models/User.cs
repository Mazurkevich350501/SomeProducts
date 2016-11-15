using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Models.ModelState;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Models
{
    [Entity(Entity.User)]
    public class User : IUser<int>, IAvailableCompany, IActive
    {
        public int Id { get; set; }

        [AuditProperty]
        public string UserName { get; set; }

        [AuditProperty]
        public string Password { get; set; }

        [Required]
        [AuditProperty]
        public int CompanyId { get; set; } = 1;
        
        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        [AuditProperty]
        public virtual ICollection<Role> Roles { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("CompanyId", CompanyId.ToString()));
            userIdentity.AddClaim(new Claim("Id", Id.ToString()));
            return userIdentity;
        }

        public State ActiveStateId { get; set; } = State.Active;

        [ForeignKey(nameof(ActiveStateId))]
        public virtual ActiveState ActiveState { get; set; }
    }
}
