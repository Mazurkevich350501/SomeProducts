using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SomeProducts.Models.ProductModels
{
    public class ProductViewModel
    {
        public ProductViewModel Product { get; set; }
        public Dictionary<int, string> Brands { get; set; }
        public Dictionary<string, string> Colors { get; set; }
    }
}