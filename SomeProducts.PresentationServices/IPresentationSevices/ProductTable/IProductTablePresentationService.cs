﻿

using SomeProducts.PresentationServices.Models.ProductTable;

namespace SomeProducts.PresentationServices.IPresentationSevices.ProductTable
{
    public interface IProductTablePresentationService
    {
        ProductTableViewModel GetTablePage(PageInfo info);
    }
}