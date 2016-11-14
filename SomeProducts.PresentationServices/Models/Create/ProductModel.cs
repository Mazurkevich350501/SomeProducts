using System.ComponentModel.DataAnnotations;
using Resources;
using SomeProducts.CrossCutting.Interfaces;

namespace SomeProducts.PresentationServices.Models.Create
{
    public class ProductModel : IImageModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        [MaxLength(200, 
            ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "Long200Characters")]
        [Display(Name = "Name", ResourceType = typeof(LocalResource))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(LocalResource))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        [Range(1, 
            double.PositiveInfinity, 
            ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Brand", ResourceType = typeof(LocalResource))]
        public int BrandId { get; set; }

        [Required( ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Color", ResourceType = typeof(LocalResource))]
        public string Color { get; set; }

        [Range(0,
            double.PositiveInfinity, 
            ErrorMessageResourceType = typeof(LocalResource),
            ErrorMessageResourceName = "PositiveValue")]
        [Display(Name = "Quantity", ResourceType = typeof(LocalResource))]
        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public byte[] Version { get; set; }

        public int CompanyId { get; set; }
    }
}
