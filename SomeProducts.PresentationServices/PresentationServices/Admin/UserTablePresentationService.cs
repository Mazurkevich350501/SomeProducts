
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

namespace SomeProducts.PresentationServices.PresentationServices.Admin
{
    public class UserTablePresentationService : IUserTablePresentationService
    {
        private readonly IUserDao _dao;
        private static readonly Dictionary<string, string> OptionDictionary;
        private static readonly List<Filter> Filters;

        static UserTablePresentationService()
        {
            OptionDictionary = new Dictionary<string, string>
            {
                {"Id", nameof(User.Id)},
                {"UserName", nameof(User.UserName)},
            };

            Filters = new List<Filter>
            {
                new Filter() {Option = nameof(User.UserName)},
                new Filter() {Option = nameof(User.Id)}
            };
        }

        public UserTablePresentationService(IUserDao dao)
        {
            _dao = dao;
        }

        public UserTableViewModel GetUserTableViewModel(PageInfo pageInfo, FilterInfo filterInfo)
        {
            var sortingOption = SortingOptionHelper.GetOptionValue(pageInfo.SortingOption, OptionDictionary);
            var productList = GetFilteredAndSortedUsers(sortingOption, filterInfo);
            var tableList = productList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount).Select(UserTableModelCast).AsQueryable();

            var result = new UserTableViewModel
            {
                Users = new StaticPagedList<UserModel>(tableList, pageInfo.Page, pageInfo.ItemsCount,
                    pageInfo.TotalItemsCount),
                PageInfo = SetPageInfo(pageInfo, sortingOption.Option),
                FilterInfo = InitFilterInfo(filterInfo),
                StringFilterParameter = DataFiltration.GetStringFilterParameter(),
                NumberFilterParameter = DataFiltration.GetNumberFilterParameter()
            };
            result.JsonFilters = DataFiltration.GetReturnedJsonFilterList(result.FilterInfo.Filters);

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

        private static FilterInfo InitFilterInfo(FilterInfo filterInfo)
        {
            var result = new FilterInfo(Filters);
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
