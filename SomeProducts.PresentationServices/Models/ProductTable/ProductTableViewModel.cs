
using PagedList;
using System.Collections.Generic;

namespace SomeProducts.PresentationServices.Models.ProductTable
{
    public class ProductTableViewModel
    {
        public IPagedList<ProductTableModel> Products { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
