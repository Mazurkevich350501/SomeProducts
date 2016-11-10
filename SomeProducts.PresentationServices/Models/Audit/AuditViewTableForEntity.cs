
using PagedList;
using SomeProducts.CrossCutting.Filter.Model;

namespace SomeProducts.PresentationServices.Models.Audit
{
    public class AuditViewTableForEntity
    {
        public IPagedList<AuditViewTableItem> Items { get; set; }

        public PageInfo PageInfo { get; set; }

        public string Entity { get; set; }
    }
}
