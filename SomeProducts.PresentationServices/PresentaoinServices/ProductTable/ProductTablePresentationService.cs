using System.Collections.Generic;
using System.Linq;
using PagedList;
using SomeProducts.DAL.IDao;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;
using SomeProducts.DAL.Models;
using System;
using System.Collections;
using SomeProducts.PresentationServices.PresentaoinServices.ProductTable.SortingOption;

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
            InitPageInfo(info);
            var sortingOption = SortingOptionHelper.GetOptionValue(info.SortingOption);
            var model = new ProductTableViewModel();
            var productList = GetSortedProducts(sortingOption).ToPagedList(info.Page, info.ProductCount);
            var tableList = productList.Select(ProductTableModelCast);
            model.Products = new StaticPagedList<ProductTableModel>(tableList, info.Page, info.ProductCount, info.TotalProductCount);
            model.PageInfo = info;

            return model;
        }

        private void InitPageInfo(PageInfo info)
        {
            info.ProductCount = GetValidProductCountValue(info, 5, 20, 10);
            info.Page = info.Page < 0 ? 1 : info.Page;
            info.TotalProductCount = _dao.GetProductCount();
        }

        private IEnumerable<Product> GetSortedProducts(SortingOption.SortingOption option)
        {
            return option.Order == Order.Original
                ? _dao.GetSortedProducts(option.Option)
                : _dao.GetDescendingSortedProducts(option.Option);
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
