using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            info.ProductCount = GetValidProductCountValue(info, 5, 20, 10);
            info.Page = info.Page < 0 ? 1 : info.Page;

            model.PageInfo = info;
            model.Products = tableList.ToPagedList(info.Page, info.ProductCount);

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

        private static string GetShortString(string str, int length)
        {
            if (str == null || length < 0) return null;
            return str.Length < length
                ? str
                : str.Substring(0, length) + "...";
        }

        private static ProductTableModel ProductTableModelCast(Product product)
        {
            if (product == null) return null;
            return new ProductTableModel()
            {
                Brand = GetShortString(product.Brand.Name, 50),
                Name = GetShortString(product.Name, 50),
                Quantity = product.Quantity,
                Description = GetShortString(product.Description, 100),
                Color = product.Color,
                Image = product.Image,
                ImageType = product.ImageType,
                Id = product.Id
            };
        }

        private static int GetValidProductCountValue(PageInfo info, int minValue, int maxValue, int defaultvalue)
        {
            return info.ProductCount < 0 || maxValue < minValue || info.ProductCount > maxValue
                ? defaultvalue
                : info.ProductCount;
        }
    }
}
