
using System;
using System.Collections.Generic;
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.PresentationServices.IPresentationSevices.Audit;
using SomeProducts.PresentationServices.Models;
using SomeProducts.PresentationServices.Models.Audit;
using SomeProducts.DAL.Models;
using System.Linq;
using PagedList;
using SomeProducts.CrossCutting.Filter;
using SomeProducts.CrossCutting.Sorting.SortingOption;
using R = Resources.Resource;
using Type = SomeProducts.CrossCutting.Filter.Model.Type;

namespace SomeProducts.PresentationServices.PresentationServices.Audit
{
    public class AuditPresentationService : IAuditPresentationService
    {
        private readonly IAuditDao _auditDao;
        private static readonly Dictionary<string, string> OptionDictionary;

        static AuditPresentationService()
        {
            OptionDictionary = new Dictionary<string, string>()
            {
                {nameof(AuditViewTableItem.EntityId), nameof(AuditItem.EntityId)},
                {nameof(AuditViewTableItem.Entity), $"{nameof(AuditItem.AuditEntity)}.{nameof(AuditEntity.Name)}"},
                {nameof(AuditViewTableItem.CompanyName), $"{nameof(AuditItem.User)}.{nameof(User.Company)}.{nameof(Company.CompanyName)}"},
                {nameof(AuditViewTableItem.ModifiedDate), nameof(AuditItem.ModifiedDateTime)},
                {nameof(AuditViewTableItem.ModifiedField), nameof(AuditItem.ModifiedField)},
                {nameof(AuditViewTableItem.PreviousValue), nameof(AuditItem.PreviousValue)},
                {nameof(AuditViewTableItem.NextValue), nameof(AuditItem.NextValue)},
                {nameof(AuditViewTableItem.UserName), $"{nameof(AuditItem.User)}.{nameof(User.Id)}"},
                {nameof(AuditViewTableItem.Status), $"{nameof(AuditItem.Status)}.{nameof(AuditStatus.Value)}"},
            };
        } 

        public AuditPresentationService(IAuditDao auditDao)
        {
            _auditDao = auditDao;
        }
        
        public AuditViewTableForEntity GetAuditViewTableForItem(PageInfo pageInfo, string entity, int id, int? companyId)
        {
            var sortingOption = SortingOptionHelper.GetOptionValue(pageInfo.SortingOption, OptionDictionary);
            var filterInfo = CreateFilterInfoForItem(entity, id);
            var itemsList = GetFilteredAndSortedItems(sortingOption, filterInfo, companyId);
            var tableList = itemsList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount)
                .Select(AuditViewTableItemCast).AsQueryable();
            var newPageInfo = SetPageInfo(pageInfo, sortingOption.Option, itemsList);

            var result = new AuditViewTableForEntity()
            {
                Items = new StaticPagedList<AuditViewTableItem>(
                    tableList, newPageInfo.Page, newPageInfo.ItemsCount, newPageInfo.TotalItemsCount),
                PageInfo = newPageInfo,
                Entity = entity
            };

            return result;
        }

        private static FilterInfo CreateFilterInfoForItem(string entity, int id)
        {
            return new FilterInfo(new List<Filter>()
            {
                new Filter()
                {
                    Option = OptionDictionary[nameof(AuditViewTableItem.Entity)],
                    Parameter = FilterParameter.IsEqualTo,
                    Type = Type.String,
                    Value = entity
                },
                new Filter()
                {
                    Option = OptionDictionary[nameof(AuditViewTableItem.EntityId)],
                    Parameter = FilterParameter.IsEqualTo,
                    Type = Type.String,
                    Value = Convert.ToString(id)
                }
            });
        }

        public AuditViewTable GetFullAuditViewTable(PageInfo pageInfo, FilterInfo filterInfo, int? companyId)
        {
            var sortingOption = SortingOptionHelper.GetOptionValue(pageInfo.SortingOption, OptionDictionary);
            var itemsList = GetFilteredAndSortedItems(sortingOption, filterInfo, companyId);
            var tableList = itemsList.ToPagedList(pageInfo.Page, pageInfo.ItemsCount).Select(AuditViewTableItemCast).AsQueryable();
            var newFilter = InitFilterInfo(filterInfo);
            var newPageInfo = SetPageInfo(pageInfo, sortingOption.Option, itemsList);

            var result = new AuditViewTable()
            {
                Items = new StaticPagedList<AuditViewTableItem>(
                    tableList, newPageInfo.Page, newPageInfo.ItemsCount, newPageInfo.TotalItemsCount),
                PageInfo = newPageInfo,
                FilterInfo = newFilter,
                JsonFilters = DataFiltration.GetReturnedJsonFilterList(newFilter.Filters)
            };

            return result;
        }

        private static PageInfo SetPageInfo<T>(PageInfo pageInfo, string option, IQueryable<T> itemsList)
        {
            pageInfo.SortingOption = option;
            pageInfo.TotalItemsCount = itemsList.Count();
            return pageInfo;
        }

        private static ICollection<Filter> GetPageFilters()
        {   
            return new List<Filter>()
            {
                new Filter() {Option = OptionDictionary[nameof(AuditViewTableItem.CompanyName)],
                    Type = Type.String, FilterName = R.Company},
                new Filter() {Option = OptionDictionary[nameof(AuditViewTableItem.Entity)],
                    Type = Type.String, FilterName = R.Entity},
                new Filter() {Option = OptionDictionary[nameof(AuditViewTableItem.EntityId)],
                    Type = Type.Numeric, FilterName = R.EntityId},
                new Filter() {Option = OptionDictionary[nameof(AuditViewTableItem.ModifiedField)],
                    Type = Type.String, FilterName = R.ModifiedField},
                new Filter() {Option = OptionDictionary[nameof(AuditViewTableItem.NextValue)],
                    Type = Type.String, FilterName = R.NextValue},
                new Filter() {Option = OptionDictionary[nameof(AuditViewTableItem.PreviousValue)],
                    Type = Type.String, FilterName = R.PreviousValue},
                new Filter() {Option = OptionDictionary[nameof(AuditViewTableItem.Status)],
                    Type = Type.String, FilterName = R.Status},
                new Filter() {Option = OptionDictionary[nameof(AuditViewTableItem.UserName)],
                    Type = Type.String, FilterName = R.User}
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

        private IQueryable<AuditItem> GetFilteredAndSortedItems(SortingOption option, FilterInfo info, int? companyId)
        {
            var items = companyId == null
                ? _auditDao.GetAllItems()
                : _auditDao.GetCompanyItems(companyId.Value);
            return items.GetFilteredItems(info).Sort(option.Option, option.Order == Order.Reverse);
        }

        private static AuditViewTableItem AuditViewTableItemCast(AuditItem item)
        {
            return new AuditViewTableItem()
            {
                UserName = item.User.UserName,
                CompanyName = item.User.Company.CompanyName,
                Status = item.Status.Value,
                Entity = item.AuditEntity.Name,
                ModifiedDate = item.ModifiedDateTime,
                EntityId = item.EntityId,
                NextValue = item.NextValue,
                ModifiedField = item.ModifiedField,
                PreviousValue = item.PreviousValue
            };
        }
    }
}
