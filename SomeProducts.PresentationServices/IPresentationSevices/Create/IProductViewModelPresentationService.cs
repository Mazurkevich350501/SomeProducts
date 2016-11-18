using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.IPresentationSevices.Create
{
    public interface IProductViewModelPresentationService
    {
        ProductViewModel GetProductViewModel(int id);

        ProductViewModel GetProductViewModel(int companyId, int id);

        ProductViewModel GetEmptyProductViewModel();

        ProductViewModel GetLastProductViewMode();

        void CreateProductViewModel(ProductViewModel model);

        void RemoveProductViewModel(int id);

        bool UpdateProductViewModel(ProductViewModel model);
    }
}
