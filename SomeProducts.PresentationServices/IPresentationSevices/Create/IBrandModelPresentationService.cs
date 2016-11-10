using System.Collections.Generic;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.IPresentationSevices.Create
{
    public interface IBrandModelPresentationService
    {
        IEnumerable<BrandModel> GetCompanyBrands(int companyId);

        void RemoveBrand(BrandModel brand);

        void CreateBrand(BrandModel model);

        bool IsBrandModelUsing(int id);

        void SaveBrandChanges(BrandsChangeModel model);

        bool UpdateBrandModel(BrandModel model);
    }
}
