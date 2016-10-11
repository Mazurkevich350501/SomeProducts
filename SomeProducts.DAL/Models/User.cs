using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace SomeProducts.DAL.Models
{
    public class User : IUser<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
