using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.Models;

namespace SomeProducts.PresentationServices.PresentaoinServices.ProductTable.SortingOption
{
    public static class SortingOptionHelper
    {
        private static readonly Dictionary<string, string> SortingOptionDictionary;
        
        static SortingOptionHelper()
        {
            SortingOptionDictionary = new Dictionary<string, string>
            {
                {"Brand", nameof(Product.Brand)},
                {"Name", nameof(Product.Name)},
                {"Quantity", nameof(Product.Quantity)},
            };
        }

        public static SortingOption GetOptionValue(string key)
        {

            Order order;
            if (key.Substring(0, 3) == "rev")
            {
                order = Order.Reverse;
                key = key.Remove(0, 3);
            }
            else
            {
                order = Order.Original;
            }
            var option = SortingOptionDictionary.Keys.Any(k => k == key)
                ? SortingOptionDictionary[key]
                : nameof(Product.Name);

            return new SortingOption(order, option);
        }
    }
}
