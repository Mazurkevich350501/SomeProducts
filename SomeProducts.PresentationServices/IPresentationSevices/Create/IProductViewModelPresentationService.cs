using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.IPresentationSevices.Create
{
    public interface IProductViewModelPresentationService
    {
        ProductViewModel GetProductViewModel(int id);

        ProductViewModel GetProductViewModel(int companyId, int id);

        ProductViewModel GetEmtyProductViewModel(int companyId);

        ProductViewModel GetLastProductViewMode(int companyId);

        void CreateProductViewModel(ProductViewModel model, int companyId);

        void RemoveProductViewModel(int id);

        bool UpdateProductViewModel(ProductViewModel model);
    }
}
