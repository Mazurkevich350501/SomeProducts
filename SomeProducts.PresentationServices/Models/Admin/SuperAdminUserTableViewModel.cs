
using System.Collections.Generic;
using SomeProducts.CrossCutting.Filter.Model;
using PagedList;
using SomeProducts.PresentationServices.Models.Interfaces;

namespace SomeProducts.PresentationServices.Models.Admin
{
    public class SuperAdminUserTableViewModel : ITableViewModel<SuperAdminUserTableItemModel>
    {
        public IPagedList<SuperAdminUserTableItemModel> Items { get; set; }

        public Dictionary<int, string> CompaniesDictionary { get; set; }

        public PageInfo PageInfo { get; set; }

        public FilterInfo FilterInfo { get; set; }

        public string JsonFilters { get; set; }
    }
}
