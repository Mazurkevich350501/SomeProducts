
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace SomeProducts.PresentationServices.Models.Admin
{
    public class AdminUserTableItemModel
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(LocalResource))]
        public string Name { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(LocalResource))]
        public ICollection<string> Roles { get; set; }

        public int CompanyId { get; set; }
    }
}
