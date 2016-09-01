
using System.ComponentModel.DataAnnotations;

namespace SomeProducts.DAL.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BrandId { get; set; }

        public string Color { get; set; }

        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }
    }
}