using PagedList;
using SomeProducts.CrossCutting.Filter.Model;

namespace SomeProducts.PresentationServices.Models.Audit
{
    public class AuditViewTable
    {
        public IPagedList<AuditViewTableItem> Items { get; set; }

        public PageInfo PageInfo { get; set; }

        public FilterInfo FilterInfo { get; set; }

        public string JsonFilters { get; set; }
    }
}
