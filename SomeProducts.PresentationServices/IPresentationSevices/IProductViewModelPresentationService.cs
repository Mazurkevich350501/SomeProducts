using SomeProducts.PresentationServices.Models;

namespace SomeProducts.PresentationServices.IPresentationSevices
{
    public interface IProductViewModelPresentationService
    {
        ProductViewModel GetProductViewModel(int? id = null);

        ProductViewModel GetLastProductViewMode();

        void CreateProductViewModel(ProductViewModel model);

        void RemoveProductViewModel(int id);

        bool UpdateProductViewModel(ProductViewModel model);
    }
}
