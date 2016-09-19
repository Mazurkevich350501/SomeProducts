using System;
using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IPresentationSevices;
using SomeProducts.PresentationServices.Models;

namespace SomeProducts.PresentationServices.PresentaoinServices
{
    public class BrandModelPresentationService : IBrandModelPresentationService
    {
        private readonly IBrandDao _brandSevice;

        public BrandModelPresentationService(IBrandDao brandSevice)
        {
            _brandSevice = brandSevice;
        }

        public void CreateBrand(BrandModel model)
        {
            var brand = new Brand() { Id = model.Id, Name = model.Name, RowVersion = model.Version };
            _brandSevice.CreateBrand(brand);
        }

        public void RemoveBrand(int id)
        {
            _brandSevice.RemoveBrand(id);
        }

        public IEnumerable<BrandModel> GetAllItems()
        {
            var brandList = _brandSevice.GetAllItems().ToList();
            return brandList.Select(brand => new BrandModel()
            {
                Id = brand.Id,
                Name = brand.Name,
                Version = brand.RowVersion
            }).ToList();
        }

        public bool IsBrandModelUsing(int id)
        {
            return _brandSevice.IsBrandUsing(id);
        }

        public void SaveBrandChanges(BrandsChangeModel changeModel)
        {
            if (changeModel != null)
            {
                if (changeModel.RemovedBrands != null)
                {
                    foreach (var brand in changeModel.RemovedBrands)
                    {
                        RemoveBrand(brand.Id);
                    }
                }

                if (changeModel.AddedBrands != null)
                {
                    foreach (var brand in changeModel.AddedBrands)
                    {
                        CreateBrand(brand);
                    }
                }

                if (changeModel.EditedBrands != null)
                {
                    foreach (var brand in changeModel.EditedBrands)
                    {
                        UbdateBrandModel(brand);
                    }
                }
            }

        }

        public bool UbdateBrandModel(BrandModel model)
        {
            return _brandSevice.UpdateBrand(new Brand()
            {
                Id = model.Id,
                Name = model.Name,
                RowVersion = model.Version
            });
        }
    }
}
