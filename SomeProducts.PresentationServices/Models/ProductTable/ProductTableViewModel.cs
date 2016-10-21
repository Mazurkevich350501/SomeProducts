
using PagedList;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.Models.Interfaces;

namespace SomeProducts.PresentationServices.Models.ProductTable
{
    public class ProductTableViewModel : ITableViewModel<ProductTableModel>
    {
        public IPagedList<ProductTableModel> Items { get; set; }

        public PageInfo PageInfo { get; set; }

        public FilterInfo FilterInfo { get; set; }

        public string JsonFilters { get; set; }
    }
}
