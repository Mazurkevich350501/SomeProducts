
namespace SomeProducts.DAL.Models.ModelState
{
    public enum State
    {
        Active = 1,
        Disable = 2
    }

    public class ActiveState
    {
        public State Id { get; set; }

        public string Value { get; set; }
    }
}
