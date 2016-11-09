
using System;

namespace SomeProducts.PresentationServices.Models.Audit
{
    public class AuditViewTableItem
    {
        public string Entity { get; set; }

        public int EntityId { get; set; }

        public string ModifiedField { get; set; }

        public string PreviousValue { get; set; }

        public string NextValue { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Status { get; set; }

        public string UserName { get; set; }

        public string CompanyName { get; set; }
    }
}
