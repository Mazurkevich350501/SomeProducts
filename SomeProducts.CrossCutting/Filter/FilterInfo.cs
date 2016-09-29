using System.Collections.Generic;

namespace SomeProducts.CrossCutting.Filter
{
    public class FilterInfo
    {
        public ICollection<Filter> Filters { get; set; }
    }
}