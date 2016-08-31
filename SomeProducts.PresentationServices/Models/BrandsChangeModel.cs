using System.Collections.Generic;

namespace SomeProducts.PresentationServices.Models
{
    public class BrandsChangeModel
    {
        public List<BrandModel> RemovedBrands { get; set; }
        public List<BrandModel> AddedBrands { get; set; }
    }
}