using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProducts.DAL.Dao;
using SomeProducts.PresentationServices.Models;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IDao;

namespace SomeProducts.PresentationServices.Dao
{
    public class BrandModelDao : IBrandModelDao
    {
        private readonly IBrandDao _brandSevice = new BrandDao();
        public void CreateBrand(BrandModel model)
        {
            var brand = new Brand() {BrandId = model.BrandId, BrandName = model.BrandName};
            _brandSevice.CreateBrand(brand);
        }

        public void RemoveBrand(int id)
        {
            _brandSevice.RemoveBrand(id);
        }

        public IEnumerable<BrandModel> GetAllItems()
        {
            var brandList = _brandSevice.GetAllItems().ToList();
            return brandList.Select(brand => new BrandModel() {BrandId = brand.BrandId, BrandName = brand.BrandName}).ToList();
        }
    }
}
