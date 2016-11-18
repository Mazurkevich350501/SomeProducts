using System.Collections.Generic;
using System.Linq;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.PresentationServices.Create
{
    public class BrandModelPresentationService : IBrandModelPresentationService
    {
        private readonly IBrandDao _brandDao;
        private readonly IUserHelper _user;

        public BrandModelPresentationService(IBrandDao brandSevice, IUserHelper user)
        {
            _brandDao = brandSevice;
            _user = user;
        }

        public void CreateBrand(BrandModel model)
        {
            var brand = BrandModelCast(model);
            _brandDao.CreateBrand(brand);
        }

        public void RemoveBrand(BrandModel model)
        {
            var brand = _brandDao.GetById(model.Id);
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

        public bool IsBrandModelUsing(int id)
        {
            return _brandDao.IsBrandUsing(id);
        }

        public void SaveBrandChanges(BrandsChangeModel changeModel)
        {
            if (changeModel == null) return;
            CreateBrands(changeModel.AddedBrands);
            RemoveBrands(changeModel.RemovedBrands);
            EditBrands(changeModel.EditedBrands);
        }

        private void RemoveBrands(ICollection<BrandModel> brands)
        {
            if (brands == null) return;
            foreach (var brand in brands)
            {
                brand.CompanyId = _user.GetCompany();
                RemoveBrand(brand);
            }
        }

        private void CreateBrands(ICollection<BrandModel> brands)
        {
            if (brands == null) return;
            foreach (var brand in brands)
            {
                brand.CompanyId = _user.GetCompany();
                CreateBrand(brand);
            }
        }

        private void EditBrands(ICollection<BrandModel> brands)
        {
            if (brands == null) return;
            foreach (var brand in brands)
            {
                brand.CompanyId = _user.GetCompany();
                UpdateBrandModel(brand);
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
