
using System.Collections.Generic;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface IBrandDao
    {
        IEnumerable<Brand> GetCompanyBrands(int companyId);

        void RemoveBrand(Brand brand, int userId);

        void CreateBrand(Brand brand, int userId);

        bool IsBrandUsing(int companyId, int id);
       
        bool UpdateBrand(Brand brand, int userId);

        Brand GetById(int companyId, int id);
    }
}
