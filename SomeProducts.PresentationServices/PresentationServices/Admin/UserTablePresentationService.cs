
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PagedList;
using SomeProducts.CrossCutting.Filter;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.CrossCutting.Sorting.SortingOption;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IPresentationSevices.Admin;
using SomeProducts.PresentationServices.Models;
using SomeProducts.PresentationServices.Models.Admin;
using System.Threading.Tasks;
using System;

namespace SomeProducts.PresentationServices.PresentationServices.Admin
{
    public class UserTablePresentationService : IUserTablePresentationService
    {
        private readonly IUserDao _dao;
        private static readonly Dictionary<string, string> SortingOptionDictionary;

        static UserTablePresentationService()
        {
            SortingOptionDictionary = new Dictionary<string, string>
            {
                {"Id", nameof(User.Id)},
                {"Name", nameof(User.UserName)},
            };
        }

        public UserTablePresentationService(IUserDao dao)
        {
            _dao = dao;
        }

        public UserTableViewModel GetUserTableViewModel(PageInfo pageInfo, FilterInfo filterInfo)
        {
            InitPageInfo(pageInfo);
            var sortingOption = GetOptionValue(pageInfo.SortingOption);
            var userList = GetFilteredAndSortedUsers(sortingOption, filterInfo);
            var tableList = userList.ToPagedList(pageInfo.Page, pageInfo.ProductCount).Select(UserTableModelCast).AsQueryable();

            var result = new UserTableViewModel
            {
                Users = new StaticPagedList<UserModel>(tableList, pageInfo.Page, pageInfo.ProductCount,pageInfo.TotalProductCount),
                PageInfo = pageInfo,
                FilterInfo = InitFilterInfo(filterInfo),
                StringFilterParameter = DataFiltration.GetStringFilterParameter(),
                NumberFilterParameter = DataFiltration.GetNumberFilterParameter()
            };
            result.JsonFilters = GetReturnedJsonFilterList(result.FilterInfo.Filters);
            return result;
        }

        private void InitPageInfo(PageInfo info)
        {
            info.ProductCount = GetValidProductCountValue(info, 5, 20, 10);
            info.Page = info.Page < 0 ? 1 : info.Page;
            info.TotalProductCount = _dao.GetUserCount();
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

        private IQueryable<User> GetFilteredAndSortedUsers(SortingOption option, FilterInfo info)
        {
            return _dao.GetAllUsers().AsQueryable().GetFilteredProduct(info).Sort(option.Option, option.Order == Order.Reverse);
        }

        private static UserModel UserTableModelCast(User user)
        {
            if (user == null) return null;
            return new UserModel()
            {
                Roles = user.Roles.Select(r => r.Name).ToList(),
                Name = user.UserName,
                Id = user.Id
            };
        }

        private static FilterInfo GetDefaultFilterInfo()
        {
            return new FilterInfo()
            {
                Filters = new List<Filter>()
                {
                    new Filter() {Option = "Id"},
                    new Filter() {Option = "UserName"}
                }
            };
        }

        private static FilterInfo InitFilterInfo(FilterInfo filterInfo)
        {
            var result = GetDefaultFilterInfo();
            if (filterInfo?.Filters == null) return result;

            foreach (var filter in filterInfo.Filters)
            {
                result.Filters.First(f => f.Option == filter.Option).Parameter = filter.Parameter;
                result.Filters.First(f => f.Option == filter.Option).Value = filter.Value;
            }
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

        public async Task ChangeAdminRole(int userId)
        {
            var user = await _dao.FindByIdAsync(userId);
            if (await  _dao.IsInRoleAsync(user, "Admin"))
            {
                await _dao.RemoveFromRoleAsync(user, "Admin");
            }
            else
            {
                await _dao.AddToRoleAsync(user, "Admin");
            }
        }

        public async Task<bool> IsUserAdmin(int userId)
        {
            return await _dao.IsInRoleAsync(await _dao.FindByIdAsync(userId), "Admin");
        }

        public async Task RemoveUser(int userId)
        {
            await _dao.DeleteAsync(await _dao.FindByIdAsync(userId));
        }
    }
}
