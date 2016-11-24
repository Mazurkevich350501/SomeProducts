

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace SomeProducts.PresentationServices.Models.Admin
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }

        [Display(Name = "Company", ResourceType = typeof(LocalResource))]
        public string CompanyName { get; set; }

        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }
    }
}
