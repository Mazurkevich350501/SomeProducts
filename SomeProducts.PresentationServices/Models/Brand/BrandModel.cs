using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models.Brand
{
    public class BrandModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        [Display(Name = "Brand name")]
        public string Name { get; set; }

        public byte[] Version { get; set; }
    }
}
