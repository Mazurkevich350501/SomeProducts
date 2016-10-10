

using System.Collections.Generic;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.Models.ProductTable;

namespace SomeProducts.PresentationServices.IPresentationSevices.ProductTable
{
    public interface IProductTablePresentationService
    {
        ProductTableViewModel GetTablePage(PageInfo pageInfo, FilterInfo filterInfo);

        IDictionary<FilterParameter, string> GetNumberFilterParameter();

        IDictionary<FilterParameter, string> GetStringFilterParameter();
    }
}
