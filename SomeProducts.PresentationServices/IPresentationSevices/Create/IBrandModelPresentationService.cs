using System.Collections.Generic;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.IPresentationSevices.Create
{
    public interface IBrandModelPresentationService
    {
        IEnumerable<BrandModel> GetCompanyBrands(int companyId);

        void RemoveBrand(BrandModel brand);

        void CreateBrand(BrandModel model);

        bool IsBrandModelUsing(int company, int id);

        void SaveBrandChanges(BrandsChangeModel model, int companyId);

        bool UpdateBrandModel(BrandModel model);
    }
}
