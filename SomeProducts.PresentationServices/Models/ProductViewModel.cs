using System.Collections.Generic;

namespace SomeProducts.PresentationServices.Models
{
    public class ProductViewModel
    {
        public ProductModel Product { get; set; }
        public Dictionary<int, string> Brands { get; set; }
        public Dictionary<string, string> Colors { get; set; }
    }
}