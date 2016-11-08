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

        public void CreateBrand(BrandModel model, int userId)
        {
            var brand = BrandModelCast(model);
            _brandDao.CreateBrand(brand, userId);
        }

        public void RemoveBrand(BrandModel model, int userId)
        {
            var brand = _brandDao.GetById(model.CompanyId, model.Id);
            _brandDao.RemoveBrand(brand);
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

        public void SaveBrandChanges(BrandsChangeModel changeModel, int companyId, int userId)
        {
            if (changeModel == null) return;
            RemoveBrands(changeModel.RemovedBrands, companyId);
            CreateBrands(changeModel.AddedBrands, companyId);
            EditBrands(changeModel.EditedBrands, companyId);
        }

        private void RemoveBrands(ICollection<BrandModel> brands, int companyId)
        {
            if (brands == null) return;
            foreach (var brand in brands)
            {
                brand.CompanyId = companyId;
                RemoveBrand(brand);
            }
        }

        private void CreateBrands(ICollection<BrandModel> brands, int companyId)
        {
            if (brands == null) return;
            foreach (var brand in brands)
            {
                brand.CompanyId = companyId;
                CreateBrand(brand);
            }
        }

        private void EditBrands(ICollection<BrandModel> brands, int companyId)
        {
            if (brands == null) return;
            foreach (var brand in brands)
            {
                brand.CompanyId = companyId;
                UpdateBrandModel(brand);
            }
        }

        public bool UpdateBrandModel(BrandModel model, int userId)
        {
            var brand = BrandModelCast(model);
            return _brandDao.UpdateBrand(brand, userId);
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
