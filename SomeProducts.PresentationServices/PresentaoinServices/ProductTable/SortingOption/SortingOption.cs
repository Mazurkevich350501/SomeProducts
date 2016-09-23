
namespace SomeProducts.PresentationServices.PresentaoinServices.ProductTable.SortingOption
{
    public enum Order
    {
        Reverse = int.MinValue,
        Original = int.MaxValue, 
    }


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
