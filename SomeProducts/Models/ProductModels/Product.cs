using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SomeProducts.Models.ProductModels
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        [Display(Name = "Product name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, double.PositiveInfinity, ErrorMessage = "The {0} field is required")]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Required]

        public string Color { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "The {0} should be positive")]
        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}