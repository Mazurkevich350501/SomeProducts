﻿
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
using Microsoft.AspNet.Identity;
using R = Resources.Resource;

namespace SomeProducts.PresentationServices.PresentationServices.Admin
{
    public class UserTablePresentationService : IUserTablePresentationService
    {
        private readonly IUserDao _userDao;
        private readonly ICompanyDao _companyDao;
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

        public UserTablePresentationService(IUserDao dao, ICompanyDao companyDao)
        {
            _userDao = dao;
            _companyDao = companyDao;
        }

        public AdminUserTableViewModel GetAdminUserTableViewModel(PageInfo pageInfo, FilterInfo filterInfo, int companyId)
        {
            var sortingOption = SortingOptionHelper.GetOptionValue(pageInfo.SortingOption, OptionDictionary);
            var newPageInfo = SetPageInfo(pageInfo, sortingOption.Option, companyId);
            var productList = GetFilteredAndSortedUsers(sortingOption, filterInfo, companyId);
            var newFilter = InitFilterInfo(filterInfo, false);
            //proverochka
            var tableList = productList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount).Select(AdminUserTableModelCast).AsQueryable();
            //-----------------------

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
            var productList = GetFilteredAndSortedUsers(sortingOption, filterInfo, null);

            var tableList = productList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount).Select(SuperAdminUserTableModelCast).AsQueryable();
            var newFilter = InitFilterInfo(filterInfo, true);
            var newPageInfo = SetPageInfo(pageInfo, sortingOption.Option, null);

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

        private PageInfo SetPageInfo(PageInfo pageInfo, string option, int? companyId)
        {
            pageInfo.SortingOption = option;
            pageInfo.TotalItemsCount = _userDao.GetUserCount(companyId);
            return pageInfo;
        }

        private IQueryable<User> GetFilteredAndSortedUsers(SortingOption option, FilterInfo info, int? companyId)
        {
            var users = companyId == null
                ? _userDao.GetAllUsers()
                : _userDao.GetAllUsers().Where(u => u.CompanyId == companyId.Value 
                    || u.CompanyId == CrossCutting.Constants.Constants.EmtyCompanyId);
            return users.AsQueryable().GetFilteredProduct(info).Sort(option.Option, option.Order == Order.Reverse);
        }

        private static AdminUserTableItemModel AdminUserTableModelCast(User user)
        {
            if (user == null) return null;
            return new AdminUserTableItemModel()
            {
                Roles = user.Roles.Select(r => r.Name).ToList(),
                Name = user.UserName,
                Id = user.Id
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
                CompanyName = user.Company.CompanyName
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
                    Option = $"{nameof(User.Company)}_{nameof(Company.CompanyName)}",
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
            if (await _userDao.IsInRoleAsync(user, nameof(UserRole.Admin)))
            {
                await _userDao.RemoveFromRoleAsync(user, nameof(UserRole.Admin));
            }
            else
            {
                await _userDao.AddToRoleAsync(user, nameof(UserRole.Admin));
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
            user.CompanyId = companyId;
            await _userDao.UpdateAsync(user);
        }

        public async Task<string> GetUserCompany(int userId)
        {
            return (await _userDao.FindByIdAsync(userId)).Company.CompanyName;
        }
    }
}
