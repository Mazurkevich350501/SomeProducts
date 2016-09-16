
using System;
using System.ComponentModel.DataAnnotations;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Models
{
    public class Brand : IDateModified, IIdentify
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}