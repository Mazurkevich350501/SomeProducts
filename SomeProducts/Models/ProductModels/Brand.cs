﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SomeProducts.Models.ProductModels
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Name cannot be longer than 200 characters.")]
        [Display(Name = "Product name")]
        public string BrandName { get; set; }
    }
}