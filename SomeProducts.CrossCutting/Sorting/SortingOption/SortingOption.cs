
namespace SomeProducts.CrossCutting.Sorting.SortingOption
{
    public enum Order { Reverse, Original }


    public class SortingOption
    {
        public SortingOption(Order order, string option)
        {
            Order = order;
            Option = option;
        }

        public Order Order { get; set; }

        public string Option { get; }
    }
}
