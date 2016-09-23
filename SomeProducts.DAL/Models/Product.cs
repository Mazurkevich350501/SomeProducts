using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Models
{
    public class Product : IDateModified, IIdentify
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }

        public string Color { get; set; }

        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}