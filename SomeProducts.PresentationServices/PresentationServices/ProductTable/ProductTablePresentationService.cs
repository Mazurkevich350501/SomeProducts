using System.Collections.Generic;
using System.Linq;
using PagedList;
using SomeProducts.CrossCutting.Filter;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;
using SomeProducts.CrossCutting.Sorting.SortingOption;
using SomeProducts.PresentationServices.Models;
using R = Resources.Resource;

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
                {nameof(ProductTableModel.Name), nameof(Product.Name)},
                {nameof(ProductTableModel.Brand), $"{nameof(Product.Brand)}.{nameof(Brand.Name)}"},
                {nameof(ProductTableModel.Quantity), nameof(Product.Quantity)},
            };
        }

        public ProductTableViewModel GetTablePage(PageInfo pageInfo, FilterInfo filterInfo)
        {
            var sortingOption = SortingOptionHelper.GetOptionValue(pageInfo.SortingOption, SortingOptionDictionary);
            var productList = GetFilteredAndSortedProducts(sortingOption, filterInfo);
            var tableList = productList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount).Select(ProductTableModelCast).AsQueryable();
            var newFilter = InitFilterInfo(filterInfo);
            var newPageInfo = SetPageInfo(pageInfo, sortingOption.Option);

            var result = new ProductTableViewModel
            {
                Items = new StaticPagedList<ProductTableModel>(
                    tableList, newPageInfo.Page, newPageInfo.ItemsCount, newPageInfo.TotalItemsCount),
                PageInfo = newPageInfo,
                FilterInfo = newFilter,
                JsonFilters = DataFiltration.GetReturnedJsonFilterList(newFilter.Filters)
            };

            return result;
        }

        private PageInfo SetPageInfo(PageInfo pageInfo, string option)
        {
            pageInfo.SortingOption = option;
            pageInfo.TotalItemsCount = _dao.GetProductCount();
            return pageInfo;
        }

        private static ICollection<Filter> GetPageFilters()
        {
            return new List<Filter>()
            {
                new Filter() {Option = nameof(ProductTableModel.Name), Type = Type.String, FilterName = R.Name},
                new Filter() {Option = nameof(ProductTableModel.Description), Type = Type.String, FilterName = R.Description},
                new Filter() {Option = $"{nameof(Brand)}_{nameof(Brand.Name)}", Type = Type.String, FilterName = R.Brand},
                new Filter() {Option = nameof(ProductTableModel.Quantity), Type = Type.Numeric, FilterName = R.Quantity}
            };
        }

        private static FilterInfo InitFilterInfo(FilterInfo filterInfo)
        {
            var result = new FilterInfo(GetPageFilters());
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
    }
}
