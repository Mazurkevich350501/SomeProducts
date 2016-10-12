using System.Collections.Generic;
using System.Linq;
using PagedList;
using SomeProducts.CrossCutting.Filter;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;
using Newtonsoft.Json;
using SomeProducts.CrossCutting.Sorting.SortingOption;
using SomeProducts.PresentationServices.Models;

namespace SomeProducts.PresentationServices.PresentationServices.ProductTable
{
    public class ProductTablePresentationService : IProductTablePresentationService
    {
        private readonly IProductDao _dao;
        private static readonly Dictionary<string, string> SortingOptionDictionary;

        public ProductTablePresentationService(IProductDao dao)
        {
            _dao = dao;
        }

        static ProductTablePresentationService()
        {
            SortingOptionDictionary = new Dictionary<string, string>
            {
                {"Brand", $"{nameof(Product.Brand)}.{nameof(Brand.Name)}"},
                {"Name", nameof(Product.Name)},
                {"Quantity", nameof(Product.Quantity)},
            };
        }

        public ProductTableViewModel GetTablePage(PageInfo pageInfo, FilterInfo filterInfo)
        {
            InitPageInfo(pageInfo);
            var sortingOption = GetOptionValue(pageInfo.SortingOption);
            var productList = GetFilteredAndSortedProducts(sortingOption, filterInfo);
            var tableList = productList.ToPagedList(pageInfo.Page, pageInfo.ProductCount).Select(ProductTableModelCast).AsQueryable();


            var result = new ProductTableViewModel
            {
                Products = new StaticPagedList<ProductTableModel>(tableList, pageInfo.Page, pageInfo.ProductCount,
                    pageInfo.TotalProductCount),
                PageInfo = pageInfo,
                FilterInfo = InitFilterInfo(filterInfo),
                StringFilterParameter = DataFiltration.GetStringFilterParameter(),
                NumberFilterParameter = DataFiltration.GetNumberFilterParameter()
            };
            result.JsonFilters = GetReturnedJsonFilterList(result.FilterInfo.Filters);

            return result;
        }

        private static string GetReturnedJsonFilterList(IEnumerable<Filter> list)
        {
            var filters = new List<Filter>();
            foreach (var filter in list)
            {
                if (filter.Parameter == FilterParameter.IsEmty || filter.Parameter == FilterParameter.IsNotEmty
                    || filter.Parameter == FilterParameter.IsNotNull || filter.Parameter == FilterParameter.IsNull)
                {
                    filters.Add(filter);
                }
                else if (filter.Value != null)
                {
                    filters.Add(filter);
                }
            }
            return JsonConvert.SerializeObject(filters);
        }

        private static FilterInfo InitFilterInfo(FilterInfo filterInfo)
        {
            var result = GetDefaultFilterInfo();
            if (filterInfo?.Filters != null)
            {
                foreach (var filter in filterInfo.Filters)
                {
                    result.Filters.First(f => f.Option == filter.Option).Parameter = filter.Parameter;
                    result.Filters.First(f => f.Option == filter.Option).Value = filter.Value;
                }
            }
            return result;
        }

        private static FilterInfo GetDefaultFilterInfo()
        {
            return new FilterInfo()
            {
                Filters = new List<Filter>()
                {
                    new Filter() {Option = "Name"},
                    new Filter() {Option = "Description"},
                    new Filter() {Option = "Brand_Name"},
                    new Filter() {Option = "Quantity"},
                }
            };
        }

        private void InitPageInfo(PageInfo info)
        {
            info.ProductCount = GetValidProductCountValue(info, 5, 20, 10);
            info.Page = info.Page < 0 ? 1 : info.Page;
            info.TotalProductCount = _dao.GetProductCount();
        }

        private IQueryable<Product> GetFilteredAndSortedProducts(SortingOption option, FilterInfo info)
        {
            return _dao.GetAllProducts().AsQueryable().GetFilteredProduct(info).Sort(option.Option, option.Order == Order.Reverse);
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

        public static SortingOption GetOptionValue(string key)
        {
            Order order;
            if (key.Length > 3 && key.Substring(0, 3) == "rev")
            {
                order = Order.Reverse;
                key = key.Remove(0, 3);
            }
            else
            {
                order = Order.Original;
            }
            var option = SortingOptionDictionary.Keys.Any(k => k == key)
                ? SortingOptionDictionary[key]
                : nameof(Product.Name);

            return new SortingOption(order, option);
        }
    }
}
