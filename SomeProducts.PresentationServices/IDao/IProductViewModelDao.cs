using SomeProducts.PresentationServices.Models;

namespace SomeProducts.PresentationServices.IDao
{
    public interface IProductViewModelDao
    {
        ProductViewModel GetProductViewModel(int? id);

        ProductViewModel GetLastProductViewMode();

        void CreateProductViewModel(ProductViewModel model);

        void RemoveProductViewModel(int id);

        void UpdateProductViewModel(ProductViewModel model);
    }
}
