
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Models.ModelState;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Models
{
    [Entity(Entity.Brand)]
    public class Brand : IDateModified, IIdentify, IAvailableCompany, IActive
    {
        public int Id { get; set; }

        [AuditProperty]
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [AuditProperty]
        public int CompanyId { get; set; }

        public State ActiveStateId { get; set; } = State.Active;

        [ForeignKey(nameof(ActiveStateId))]
        public virtual ActiveState ActiveState { get; set; }
    }
}