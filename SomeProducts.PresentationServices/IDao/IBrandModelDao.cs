using System.Collections.Generic;
using SomeProducts.PresentationServices.Models;

namespace SomeProducts.PresentationServices.IDao
{
    public interface IBrandModelDao
    {
        IEnumerable<BrandModel> GetAllItems();

        void RemoveBrand(int id);

        void CreateBrand(BrandModel model);

        bool IsBrandModelUsing(int id);
    }
}
