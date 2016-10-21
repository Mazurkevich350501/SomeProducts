
using SomeProducts.CrossCutting.Filter.Model;
using PagedList;
using SomeProducts.PresentationServices.Models.Interfaces;

namespace SomeProducts.PresentationServices.Models.Admin
{
    public class UserTableViewModel : ITableViewModel<UserModel>
    {
        public IPagedList<UserModel> Items { get; set; }

        public PageInfo PageInfo { get; set; }

        public FilterInfo FilterInfo { get; set; }

        public string JsonFilters { get; set; }
    }
}
