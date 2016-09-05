using SomeProducts.PresentationServices.Models;

namespace SomeProducts.PresentationServices.IPresentationSevices
{
    public interface IProductViewModelPresentationService
    {
        ProductViewModel GetProductViewModel(int? id);

        ProductViewModel GetLastProductViewMode();

        void CreateProductViewModel(ProductViewModel model);

        void RemoveProductViewModel(int id);

        void UpdateProductViewModel(ProductViewModel model);
    }
}
