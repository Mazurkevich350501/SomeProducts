using System.ComponentModel.DataAnnotations;
using Resources;

namespace SomeProducts.PresentationServices.Models.Create
{
    public class BrandModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        [MaxLength(200, 
            ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "Long200Characters")]
        [Display(Name = "Brand", ResourceType = typeof(LocalResource))]
        public string Name { get; set; }

        public int CompanyId { get; set; }

        public byte[] Version { get; set; }
    }
}
