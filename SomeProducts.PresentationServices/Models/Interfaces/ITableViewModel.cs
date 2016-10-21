

using PagedList;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.Models.ProductTable;

namespace SomeProducts.PresentationServices.Models.Interfaces
{
    public interface ITableViewModel<T>
    {
        IPagedList<T> Items { get; set; }

        PageInfo PageInfo { get; set; }

        FilterInfo FilterInfo { get; set; }

        string JsonFilters { get; set; }
    }
}
