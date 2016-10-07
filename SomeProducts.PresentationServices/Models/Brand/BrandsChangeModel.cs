using System.Collections.Generic;

namespace SomeProducts.PresentationServices.Models.Brand
{
    public class BrandsChangeModel
    {
        public List<BrandModel> RemovedBrands { get; set; }

        public List<BrandModel> AddedBrands { get; set; }

        public List<BrandModel> EditedBrands { get; set; }
    }
}