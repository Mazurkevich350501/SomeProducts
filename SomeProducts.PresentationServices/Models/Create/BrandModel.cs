using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models.Create
{
    public class BrandModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [MaxLength(200, 
            ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "Long200Characters")]
        [Display(Name = "Brand", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        public byte[] Version { get; set; }
    }
}
