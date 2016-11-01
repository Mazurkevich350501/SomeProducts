
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models.Admin
{
    public class AdminUserTableItemModel
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(Resources.Resource))]
        public ICollection<string> Roles { get; set; }

        public int CompanyId { get; set; }
    }
}
