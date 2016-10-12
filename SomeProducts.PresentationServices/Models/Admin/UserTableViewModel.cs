
using SomeProducts.CrossCutting.Filter.Model;
using System.Collections.Generic;
using PagedList;

namespace SomeProducts.PresentationServices.Models.Admin
{
    public class UserTableViewModel
    {
        public IPagedList<UserModel> Users { get; set; }

        public PageInfo PageInfo { get; set; }

        public FilterInfo FilterInfo { get; set; }

        public string JsonFilters { get; set; }

        public IDictionary<FilterParameter, string> NumberFilterParameter { get; set; }

        public IDictionary<FilterParameter, string> StringFilterParameter { get; set; }
    }
}
