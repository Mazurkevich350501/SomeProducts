using System.Collections.Generic;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.IPresentationSevices.Create
{
    public interface IBrandModelPresentationService
    {
        IEnumerable<BrandModel> GetCompanyBrands(int companyId);

        void RemoveBrand(BrandModel brand, int userId);

        void CreateBrand(BrandModel model, int userId);

        bool IsBrandModelUsing(int company, int id);

        void SaveBrandChanges(BrandsChangeModel model, int companyId, int userId);

        bool UpdateBrandModel(BrandModel model, int userId);
    }
}
