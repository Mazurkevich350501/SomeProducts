
using System;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Models
{
    public class Brand : IDateModified
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}