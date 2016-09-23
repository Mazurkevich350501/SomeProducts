
namespace SomeProducts.PresentationServices.Models.ProductTable
{
    public class PageInfo
    {
        public int Page { get; set; }

        public int ProductCount { get; set; }

        public int TotalProductCount { get; set; }

        public string SortingOption { get; set; }
    }
}
