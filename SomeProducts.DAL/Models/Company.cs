
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SomeProducts.DAL.Models.ModelState;
using SomeProducts.DAL.Repository.Interface.Model;

namespace SomeProducts.DAL.Models
{
    public class Company : IActive
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

        public State ActiveStateId { get; set; } = State.Active;

        [ForeignKey(nameof(ActiveStateId))]
        public virtual ActiveState ActiveState { get; set; }
    }
}