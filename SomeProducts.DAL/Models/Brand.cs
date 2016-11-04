
using System;
using System.ComponentModel.DataAnnotations;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Models
{
    [Entity(Entity.Brand)]
    public class Brand : IDateModified, IIdentify, IAvailableCompany
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
    }
}