using System.Collections.Generic;
using SomeProducts.PresentationServices.Models.Brand;

namespace SomeProducts.PresentationServices.IPresentationSevices
{
    public interface IBrandModelPresentationService
    {
        IEnumerable<BrandModel> GetAllItems();

        void RemoveBrand(BrandModel brand);

        void CreateBrand(BrandModel model);

        bool IsBrandModelUsing(int id);

        void SaveBrandChanges(BrandsChangeModel model);

        bool UbdateBrandModel(BrandModel model);
    }
}
