using System.Collections.Generic;
using System.Linq;
using PagedList;
using SomeProducts.DAL.IDao;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;
using SomeProducts.DAL.Models;

namespace SomeProducts.PresentationServices.PresentaoinServices.ProductTable
{
    public class ProductTablePresentationService : IProductTablePresentationService
    {
        private readonly IProductDao _dao;

        public ProductTablePresentationService(IProductDao dao)
        {
            _dao = dao;
        }


        public ProductTableViewModel GetTablePage(PageInfo info)
        {
            var model = new ProductTableViewModel();
            var productList = GetSortedProducts(info.SortingOption);

            var tableList = productList.Select(ProductTableModelCast).ToList();
            model.Products = tableList.ToPagedList(info.Page, info.ProductCount);
            model.PageInfo = info;
            return model;
        }

        private IEnumerable<Product> GetSortedProducts(string sortingOption)
        {
            var sortedBy = SortingOptionHelper.GetOptionValue(sortingOption);
            var sortingParam = sortedBy.Replace("rev", "");
            var result = sortingParam == "BrandName" ? 
                _dao.GetSortedByBrandsProducts(sortingParam.Replace("Brand","")).ToList() : 
                _dao.GetSortedProducts(sortingParam).ToList();
            if (sortedBy.Substring(0, 3) == "rev") result.Reverse();

            return result;
        }

        private static ProductTableModel ProductTableModelCast(Product product)
        {
            if (product == null) return null;
            return new ProductTableModel()
            {
                Brand = product.Brand.Name,
                Name = product.Name,
                Quantity = product.Quantity,
                Description = product.Description,
                Color = product.Color,
                Image = product.Image,
                ImageType = product.ImageType,
                Id = product.Id
            };
        }
    }
}
