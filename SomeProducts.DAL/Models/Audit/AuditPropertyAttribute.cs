
using System;

namespace SomeProducts.DAL.Models.Audit
{
    public class AuditPropertyAttribute : Attribute
    {
        public AuditPropertyAttribute(bool isAuditing)
        {
            IsAuditing = isAuditing;
        }

        public AuditPropertyAttribute()
        {
            IsAuditing = true;
        }

        public bool IsAuditing { get; }
    }
}
