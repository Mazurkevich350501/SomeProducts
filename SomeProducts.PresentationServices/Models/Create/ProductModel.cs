using System.ComponentModel.DataAnnotations;
using SomeProducts.CrossCutting.Interfaces;

namespace SomeProducts.PresentationServices.Models.Create
{
    public class ProductModel : IImageModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [MaxLength(200, 
            ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "Long200Characters")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Resource))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [Range(1, 
            double.PositiveInfinity, 
            ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Brand", ResourceType = typeof(Resources.Resource))]
        public int BrandId { get; set; }

        [Required( ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Color", ResourceType = typeof(Resources.Resource))]
        public string Color { get; set; }

        [Range(0,
            double.PositiveInfinity, 
            ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "PositiveValue")]
        [Display(Name = "Quantity", ResourceType = typeof(Resources.Resource))]
        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public byte[] Version { get; set; }

        public int CompanyId { get; set; }
    }
}
