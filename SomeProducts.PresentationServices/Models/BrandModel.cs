﻿using System.ComponentModel.DataAnnotations;

namespace SomeProducts.PresentationServices.Models
{
    public class BrandModel
    {
        public int BrandId;
        [Required]
        [MaxLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        [Display(Name = "Product name")]
        public string BrandName;
    }
}