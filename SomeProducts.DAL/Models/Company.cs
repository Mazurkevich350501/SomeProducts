
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}