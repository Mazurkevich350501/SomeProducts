
using System.Collections.Generic;
using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface IBrandDao
    {
        IEnumerable<Brand> GetCompanyBrands(int companyId);

        void RemoveBrand(Brand brand);

        void CreateBrand(Brand brand);

        bool IsBrandUsing(int id);
       
        bool UpdateBrand(Brand brand);

        Brand GetById(int id);
    }
}
