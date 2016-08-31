using System.Collections.Generic;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.Dao
{
    public class BrandDao : IBrandDao
    {
        private readonly BrandRepository _repository = new BrandRepository();

        public void CreateBrand(Brand brand)
        {
            _repository.Create(brand);
            _repository.Save();
        }

        public IEnumerable<Brand> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public void RemoveBrand(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }
    }
}
