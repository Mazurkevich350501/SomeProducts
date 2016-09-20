
using System.Collections.Generic;

namespace SomeProducts.PresentationServices.Models.ProductTable
{
    public class ProductTableViewModel
    {
        public ProductTableViewModel()
        {
            Products = new List<ProductTableModel>();
        }
        public List<ProductTableModel> Products { get; set; }
    }
}
