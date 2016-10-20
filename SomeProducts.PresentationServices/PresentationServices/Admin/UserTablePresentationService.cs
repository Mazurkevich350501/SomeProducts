
using System.Collections.Generic;
using System.Linq;
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
using R = Resources.Resource;

namespace SomeProducts.PresentationServices.PresentationServices.Admin
{
    public class UserTablePresentationService : IUserTablePresentationService
    {
        private readonly IUserDao _dao;
        private static readonly Dictionary<string, string> OptionDictionary;
        private readonly List<Filter> _filters;

        static UserTablePresentationService()
        {
            OptionDictionary = new Dictionary<string, string>
            {
                {"Id", nameof(User.Id)},
                {"UserName", nameof(User.UserName)},
            };
        }

        public UserTablePresentationService(IUserDao dao)
        {
            _dao = dao;

            _filters = new List<Filter>
            {
                new Filter()
                {
                    Option = nameof(User.Id),
                    Type = Type.Numeric,
                    FilterName = R.Id
                },
                new Filter()
                {
                    Option = nameof(User.UserName),
                    Type = Type.String,
                    FilterName = R.Name
                }
            };
        }

        public UserTableViewModel GetUserTableViewModel(PageInfo pageInfo, FilterInfo filterInfo)
        {
            var sortingOption = SortingOptionHelper.GetOptionValue(pageInfo.SortingOption, OptionDictionary);
            var productList = GetFilteredAndSortedUsers(sortingOption, filterInfo);
            var tableList = productList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount).Select(UserTableModelCast).AsQueryable();
            var newFilter = InitFilterInfo(filterInfo);
            var newPageInfo = SetPageInfo(pageInfo, sortingOption.Option);

            var result = new UserTableViewModel
            {
                Users = new StaticPagedList<UserModel>(
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
            pageInfo.TotalItemsCount = _dao.GetUserCount();
            return pageInfo;
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

        private FilterInfo InitFilterInfo(FilterInfo filterInfo)
        {
            var result = new FilterInfo(_filters);
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

        public async Task ChangeAdminRole(int userId)
        {
            var user = await _dao.FindByIdAsync(userId);
            if (await  _dao.IsInRoleAsync(user, nameof(UserRole.Admin)))
            {
                await _dao.RemoveFromRoleAsync(user, nameof(UserRole.Admin));
            }
            else
            {
                await _dao.AddToRoleAsync(user, nameof(UserRole.Admin));
            }
        }

        public async Task RemoveUser(int userId)
        {
            await _dao.DeleteAsync(await _dao.FindByIdAsync(userId));
        }

        public async Task<IList<string>> GetUserRoles(int userId)
        {
            return await _dao.GetRolesAsync(await _dao.FindByIdAsync(userId));
        }

        public bool IsUserExist(int userId, string userName)
        {
            return _dao.GetAllUsers().Any(user => user.Id == userId && user.UserName == userName);
        }
    }
}
