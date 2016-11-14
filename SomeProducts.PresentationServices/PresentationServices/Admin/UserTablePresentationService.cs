
using System.Collections.Generic;
using System.ComponentModel;
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
using R = Resources.LocalResource;

namespace SomeProducts.PresentationServices.PresentationServices.Admin
{
    public class UserTablePresentationService : IUserTablePresentationService
    {
        private readonly IUserDao _userDao;
        private readonly ICompanyDao _companyDao;
        private readonly IUserHelper _user;

        private static readonly Dictionary<string, string> OptionDictionary;

        static UserTablePresentationService()
        {
            OptionDictionary = new Dictionary<string, string>
            {
                {nameof(User.Id), nameof(User.Id)},
                {nameof(User.UserName), nameof(User.UserName)},
                {nameof(User.Company), $"{nameof(User.Company)}.{nameof(Company.CompanyName)}"}
            };
        }

        public UserTablePresentationService(IUserDao dao, ICompanyDao companyDao, IUserHelper user)
        {
            _userDao = dao;
            _companyDao = companyDao;
            _user = user;
        }

        public AdminUserTableViewModel GetAdminUserTableViewModel(PageInfo pageInfo, FilterInfo filterInfo)
        {
            var sortingOption = SortingOptionHelper.GetOptionValue(pageInfo.SortingOption, OptionDictionary);
            var userList = GetFilteredAndSortedUsers(sortingOption, filterInfo, _user.GetSuperAdminCompany());
            var newFilter = InitFilterInfo(filterInfo, _user.IsInRole(UserRole.SuperAdmin));
            var tableList = userList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount).Select(AdminUserTableModelCast).AsQueryable();
            var newPageInfo = SetPageInfo(pageInfo, sortingOption.Option, userList.Count());

            var result = new AdminUserTableViewModel
            {
                Items = new StaticPagedList<AdminUserTableItemModel>(
                    tableList, newPageInfo.Page, newPageInfo.ItemsCount, newPageInfo.TotalItemsCount),
                PageInfo = newPageInfo,
                FilterInfo = newFilter,
                JsonFilters = DataFiltration.GetReturnedJsonFilterList(newFilter.Filters)
            };

            return result;
        }

        public SuperAdminUserTableViewModel GetSuperAdminUserTableViewModel(PageInfo pageInfo, FilterInfo filterInfo)
        {
            var sortingOption = SortingOptionHelper.GetOptionValue(pageInfo.SortingOption, OptionDictionary);
            var userList = GetFilteredAndSortedUsers(sortingOption, filterInfo, _user.GetSuperAdminCompany());
            var tableList = userList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount).Select(SuperAdminUserTableModelCast).AsQueryable();
            var newFilter = InitFilterInfo(filterInfo, _user.IsInRole(UserRole.SuperAdmin));
            var newPageInfo = SetPageInfo(pageInfo, sortingOption.Option, userList.Count());

            var result = new SuperAdminUserTableViewModel
            {
                Items = new StaticPagedList<SuperAdminUserTableItemModel>(
                    tableList, newPageInfo.Page, newPageInfo.ItemsCount, newPageInfo.TotalItemsCount),
                CompaniesDictionary = GetCompanesDictionary(),
                PageInfo = newPageInfo,
                FilterInfo = newFilter,
                JsonFilters = DataFiltration.GetReturnedJsonFilterList(newFilter.Filters)
            };

            return result;
        }

        private Dictionary<int, string> GetCompanesDictionary()
        {
            return _companyDao.GetAllItems().ToDictionary(c => c.Id, c => c.CompanyName);
        }

        private static PageInfo SetPageInfo(PageInfo pageInfo, string option, int totalCount)
        {
            pageInfo.SortingOption = option;
            pageInfo.TotalItemsCount = totalCount;
            return pageInfo;
        }

        private IQueryable<User> GetFilteredAndSortedUsers(SortingOption option, FilterInfo info, int? companyId)
        {
            var users = companyId == null
                ? _userDao.GetAllUsers()
                : _userDao.GetAllUsers().Where(u => u.CompanyId == companyId.Value 
                    || u.CompanyId == CrossCutting.Constants.Constants.EmtyCompanyId);
            return users.AsQueryable().GetFilteredItems(info).Sort(option.Option, option.Order == Order.Reverse);
        }

        private static AdminUserTableItemModel AdminUserTableModelCast(User user)
        {
            if (user == null) return null;
            return new AdminUserTableItemModel()
            {
                Roles = user.Roles.Select(r => r.Name).ToList(),
                Name = user.UserName,
                Id = user.Id,
                CompanyId = user.CompanyId
            };
        }

        private static SuperAdminUserTableItemModel SuperAdminUserTableModelCast(User user)
        {
            if (user == null) return null;
            return new SuperAdminUserTableItemModel()
            {
                Roles = user.Roles.Select(r => r.Name).ToList(),
                Name = user.UserName,
                Id = user.Id,
                CompanyName = user.Company.CompanyName,
                CompanyId = user.CompanyId
            };
        }

        private static ICollection<Filter> GetPageFilters(bool isSuperAdmin)
        {
            var result = new List<Filter>
            {
                new Filter()
                {Option = nameof(User.Id), Type = Type.Numeric, FilterName = R.Id},
                new Filter()
                {Option = nameof(User.UserName), Type = Type.String, FilterName = R.Name}
            };
            if (isSuperAdmin)
            {
                result.Add(new Filter()
                {
                    Option = $"{nameof(User.Company)}.{nameof(Company.CompanyName)}",
                    Type = Type.String,
                    FilterName = R.Company
                });
            }
            return result;
        }

        private static FilterInfo InitFilterInfo(FilterInfo filterInfo, bool isSuperAdmin)
        {
            var result = new FilterInfo(GetPageFilters(isSuperAdmin));
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
            var user = await _userDao.FindByIdAsync(userId);
            if (await _userDao.IsInRoleAsync(user, UserRole.Admin.AsString()))
            {
                await _userDao.RemoveFromRoleAsync(user, UserRole.Admin.AsString());
            }
            else if (user.CompanyId != CrossCutting.Constants.Constants.EmtyCompanyId)
            {
                await _userDao.AddToRoleAsync(user, UserRole.Admin.AsString());
            }
            else
            {
                throw new WarningException("User without company cannot be in Admin role");
            }
        }

        public async Task RemoveUser(int userId)
        {
            await _userDao.DeleteAsync(await _userDao.FindByIdAsync(userId));
        }

        public async Task<IList<string>> GetUserRoles(int userId)
        {
            return await _userDao.GetRolesAsync(await _userDao.FindByIdAsync(userId));
        }

        public bool IsUserExist(int userId, string userName)
        {
            return _userDao.GetAllUsers().Any(user => user.Id == userId && user.UserName == userName);
        }

        public async Task SetUserCompany(int userId, int companyId)
        {
            var user = await _userDao.FindByIdAsync(userId);
            if (user.Roles.Any(r => r.Name == UserRole.Admin.AsString())
                && companyId == CrossCutting.Constants.Constants.EmtyCompanyId)
            {
                throw new WarningException("User without company cannot be in Admin role");
            }
            user.CompanyId = companyId;
            await _userDao.UpdateAsync(user);
        }

        public async Task<CompanyModel> GetUserCompany(int userId)
        {
            var user = await _userDao.FindByIdAsync(userId);
            return new CompanyModel()
            {
                CompanyId = user.CompanyId,
                CompanyName = user.Company.CompanyName
            };
        }
    }
}
