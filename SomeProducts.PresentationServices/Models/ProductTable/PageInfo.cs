using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProducts.PresentationServices.Models.ProductTable
{
    public class PageInfo
    {
        public int Page { get; set; }

        public int ProductCount { get; set; }

        public string SortingOption { get; set; }
    }
}
