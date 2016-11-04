using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Models
{
    [Entity(Entity.Product)]
    public class Product : IDateModified, IIdentify, IAvailableCompany
    {
        public int Id { get; set; }

        [AuditProperty]
        public string Name { get; set; }

        [AuditProperty]
        public string Description { get; set; }

        [AuditProperty]
        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }

        [AuditProperty]
        public string Color { get; set; }

        [AuditProperty]
        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required]
        [AuditProperty]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }
    }
}