
using System;
using System.ComponentModel.DataAnnotations;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Models
{
    public class Brand : IDateModified, IIdentify, IComparable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int CompareTo(object obj)
        {
            return string.Compare(Name, ((Brand)obj).Name, StringComparison.Ordinal);
        }
    }
}