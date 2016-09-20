using System.Collections.Generic;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.IPresentationSevices.Create
{
    public interface IBrandModelPresentationService
    {
        IEnumerable<BrandModel> GetAllItems();

        void RemoveBrand(int id);

        void CreateBrand(BrandModel model);

        bool IsBrandModelUsing(int id);

        void SaveBrandChanges(BrandsChangeModel model);

        bool UbdateBrandModel(BrandModel model);
    }
}
