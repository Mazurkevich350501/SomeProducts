using System.Collections.Generic;
using System.Linq;
using R = Resources.LocalResource;

namespace SomeProducts.CrossCutting.Filter.Model
{
    public class FilterInfo
    {
        public FilterInfo (ICollection<Filter> filters)
        {
            Filters = filters;
        }

        public ICollection<Filter> Filters { get; set; }

        public IDictionary<FilterParameter, string> NumberFilterParameter => new Dictionary<FilterParameter, string>()
        {
            {FilterParameter.IsEqualTo, R.IsEqualTo},
            {FilterParameter.IsNotEqualTo, R.IsNotEqualTo},
            {FilterParameter.IsGreaterThanOrEqualTo, R.IsGreaterThanOrEqualTo},
            {FilterParameter.IsLessThenOrEqualTo, R.IsLessThenOrEqualTo},
            {FilterParameter.IsLessThen, R.IsLessThen}
        };

        public IDictionary<FilterParameter, string> StringFilterParameter => new Dictionary<FilterParameter, string>()
        { 
            {FilterParameter.IsEqualTo, R.IsEqualTo},
            {FilterParameter.IsNotEqualTo, R.IsNotEqualTo},
            {FilterParameter.Contains, R.Contains},
            {FilterParameter.DoesNotContain, R.DoesNotContain},
            {FilterParameter.IsEmty, R.IsEmty},
            {FilterParameter.IsNotEmty, R.IsNotEmty},
            {FilterParameter.IsNull, R.IsNull},
            {FilterParameter.IsNotNull, R.IsNotNull},
        };

        public string FiltertsList
        {
            get
            {
                var filtersArray = Filters.Select(filter => filter.Option);
                return string.Join(",", filtersArray);
            }
        }
    }
}