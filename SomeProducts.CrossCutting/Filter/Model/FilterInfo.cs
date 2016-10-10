using System.Collections.Generic;

namespace SomeProducts.CrossCutting.Filter.Model
{
    public class FilterInfo
    {
        public ICollection<Filter> Filters { get; set; }
    }
}