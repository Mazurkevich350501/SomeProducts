
using System.Collections.Generic;
using PagedList;
using SomeProducts.CrossCutting.Filter;

namespace SomeProducts.PresentationServices.Models.ProductTable
{
    public class ProductTableViewModel
    {
        public IPagedList<ProductTableModel> Products { get; set; }

        public PageInfo PageInfo { get; set; }

        public FilterInfo FilterInfo { get; set; }

        public IDictionary<FilterParameter, string> NumberFilterParameter { get; set; }

        public IDictionary<FilterParameter, string> StringFilterParameter { get; set; }
    }
}
