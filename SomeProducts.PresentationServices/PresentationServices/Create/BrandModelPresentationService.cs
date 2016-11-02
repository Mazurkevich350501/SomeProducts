using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.PresentationServices.Create
{
    public class BrandModelPresentationService : IBrandModelPresentationService
    {
        private readonly IBrandDao _brandDao;

        public BrandModelPresentationService(IBrandDao brandSevice)
        {
            _brandDao = brandSevice;
        }

        public void CreateBrand(BrandModel model)
        {
            var brand = BrandModelCast(model);
            _brandDao.CreateBrand(brand);
        }

        public void RemoveBrand(BrandModel model)
        {
            _brandDao.RemoveBrand(BrandModelCast(model));
        }

        public IEnumerable<BrandModel> GetCompanyBrands(int companyId)
        {
            var brandList = _brandDao.GetCompanyBrands(companyId).AsQueryable();
            return brandList.Select(brand => new BrandModel()
            {
                Id = brand.Id,
                Name = brand.Name,
                Version = brand.RowVersion
            }).ToList();
        }

        public bool IsBrandModelUsing(int companyId, int id)
        {
            return _brandDao.IsBrandUsing(companyId, id);
        }

        public void SaveBrandChanges(BrandsChangeModel changeModel, int companyId)
        {
            if (changeModel != null)
            {
                if (changeModel.RemovedBrands != null)
                {
                    foreach (var brand in changeModel.RemovedBrands)
                    {
                        RemoveBrand(brand);
                    }
                }

                if (changeModel.AddedBrands != null)
                {
                    foreach (var brand in changeModel.AddedBrands)
                    {
                        brand.CompanyId = companyId;
                        CreateBrand(brand);
                    }
                }

                if (changeModel.EditedBrands != null)
                {
                    foreach (var brand in changeModel.EditedBrands)
                    {
                        brand.CompanyId = companyId;
                        UpdateBrandModel(brand);
                    }
                }
            }

        }

        public bool UpdateBrandModel(BrandModel model)
        {
            var brand = BrandModelCast(model);
            return _brandDao.UpdateBrand(brand);
        }

        private static Brand BrandModelCast(BrandModel model)
        {
            return new Brand()
            {
                Name = model.Name,
                Id = model.Id,
                RowVersion = model.Version,
                CompanyId = model.CompanyId
            };
        }
    }
}
