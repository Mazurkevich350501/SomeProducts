using SomeProducts.DAL.Models;

namespace SomeProducts.DAL.IDao
{
    public interface IProductDao
    {
        Product GetProduct(int id);

        Product GetLastProduct();

        void UpdateProduct(Product product);

        void CreateProduct(Product product);

        void RemoveProduct(int id);
    }
}
