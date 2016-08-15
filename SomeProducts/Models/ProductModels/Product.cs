using System;
using System.Collections.Generic;
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
        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Required]
        public string Color { get; set; }

        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }
    }
}