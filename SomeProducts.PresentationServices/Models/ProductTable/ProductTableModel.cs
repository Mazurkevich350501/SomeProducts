using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models.ProductTable
{
    public class ProductTableModel
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Resource))]
        public string Description { get; set; }

        [Display(Name = "Brand", ResourceType = typeof(Resources.Resource))]
        public string Brand { get; set; }

        public string Color { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(Resources.Resource))]
        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }
        
    }
}
