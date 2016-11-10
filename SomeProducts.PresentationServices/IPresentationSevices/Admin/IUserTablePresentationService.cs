﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.Models;
using SomeProducts.PresentationServices.Models.Admin;

namespace SomeProducts.PresentationServices.IPresentationSevices.Admin
{
    public interface IUserTablePresentationService
    {
        AdminUserTableViewModel GetAdminUserTableViewModel(PageInfo pageInfo, FilterInfo filterInfo);

        SuperAdminUserTableViewModel GetSuperAdminUserTableViewModel(PageInfo pageInfo, FilterInfo filterInfo);

        Task RemoveUser(int userId);

        Task ChangeAdminRole(int userId);
        
        Task<IList<string>> GetUserRoles(int userId);

        bool IsUserExist(int userId, string userName);

        Task SetUserCompany(int userId, int companyId);

        Task<CompanyModel> GetUserCompany(int userId);
    }
}