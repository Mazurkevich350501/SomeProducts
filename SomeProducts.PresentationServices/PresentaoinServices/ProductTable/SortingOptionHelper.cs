using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProducts.PresentationServices.PresentaoinServices.ProductTable
{
    public static class SortingOptionHelper
    {
        private static readonly Dictionary<string, string> SortingOptionDictionary;
        
        static SortingOptionHelper()
        {
            SortingOptionDictionary = new Dictionary<string, string>
            {
                {"Brand", "BrandName"},
                {"revBrand", "revBrandName"},
                {"Name", "Name"},
                {"revName", "revName"},
                {"Quantity", "Quantity"},
                {"revQuantity", "revQuantity"}
            };
        }

        public static string GetOptionValue(string key)
        {
            return SortingOptionDictionary.Keys.Any(k => k == key) ? SortingOptionDictionary[key] : "Name";
        }
    }
}
