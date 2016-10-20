
namespace SomeProducts.CrossCutting.Filter.Model
{
    public enum FilterParameter
    {
        IsEqualTo,
        IsNotEqualTo,
        IsGreaterThanOrEqualTo,
        IsLessThenOrEqualTo,
        IsLessThen,
        IsNull,
        IsNotNull,
        Contains,
        DoesNotContain,
        EndsWith,
        IsEmty,
        IsNotEmty
    }

    public enum Type
    {
        Numeric,
        String
    }

    public class Filter
    {
        public string Option { get; set; }

        public Type Type { get; set; }

        public FilterParameter Parameter { get; set; }

        public string Value { get; set; }

        public string FilterName { get; set; }
    }
}
