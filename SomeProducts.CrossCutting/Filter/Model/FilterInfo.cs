using System.Collections.Generic;

namespace SomeProducts.CrossCutting.Filter.Model
{
    public class FilterInfo
    {
        public FilterInfo (ICollection<Filter> filters)
        {
            Filters = filters;
        }

        public ICollection<Filter> Filters { get; set; }
    }
}