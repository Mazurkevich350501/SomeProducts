
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace SomeProducts.DAL.Models
{
    public class Role : IRole<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
