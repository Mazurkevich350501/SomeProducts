using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeProducts.DAL.Models.Audit
{
    public class AuditItem
    {
        public int Id { get; set; }

        public Entity AuditEntityId { get; set; }

        [ForeignKey(nameof(AuditEntityId))]
        public virtual AuditEntity AuditEntity { get; set; }

        public int  EntityId { get; set; }

        public string ModifiedField { get; set; }

        public string PreviousValue { get; set; }

        public string NextValue { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public Status StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual AuditStatus Status { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
