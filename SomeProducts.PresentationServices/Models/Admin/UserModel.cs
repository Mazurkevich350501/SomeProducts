
using System.Collections.Generic;

namespace SomeProducts.PresentationServices.Models.Admin
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
