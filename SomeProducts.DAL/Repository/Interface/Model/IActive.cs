
using SomeProducts.DAL.Models.ModelState;

namespace SomeProducts.DAL.Repository.Interface.Model
{
    public interface IActive
    {
        State ActiveStateId { get; set; }
    }
}
