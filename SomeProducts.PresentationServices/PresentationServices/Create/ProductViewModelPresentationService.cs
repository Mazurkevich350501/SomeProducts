
using System.Collections.Generic;
using System.Linq;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.Models.Create;

namespace SomeProducts.PresentationServices.PresentationServices.Create
{
    public class ProductViewModelPresentationService : IProductViewModelPresentationService
    {
        private readonly IProductDao _productDao;
        private readonly IBrandDao _brandDao;

        public ProductViewModelPresentationService(IProductDao productService, IBrandDao brandService)
        {
            _productDao = productService;
            _brandDao = brandService;
        }

        public void RemoveProductViewModel(int id, int companyId)
        {
            var product = _productDao.GetProduct(id);
            if (product.CompanyId == companyId)
            {
                _productDao.RemoveProduct(product);
            }
        }

        public ProductViewModel GetProductViewModel(int companyId, int id)
        {
            var result = GetProductViewModel(id);
            if (result != null && result.Product.CompanyId == companyId)
            {
                return result;
            }
            return null;
        }

        public ProductViewModel GetProductViewModel(int id)
        {
            var product = _productDao.GetProduct(id);
            if (product == null) return null;
            var productModel = ProductModelCast(product);

            return new ProductViewModel()
            {
                Product = productModel,
                Brands = CreateBrandDictionary(product.CompanyId),
                Colors = ProductColors.Colors
            };
        }

        public ProductViewModel GetEmtyProductViewModel(int companyId)
        {
            return new ProductViewModel()
            {
                Product = new ProductModel(),
                Brands = CreateBrandDictionary(companyId),
                Colors = ProductColors.Colors
            };
        }

        public bool UpdateProductViewModel(ProductViewModel model, int companyId)
        {
            var product = ProductCast(model.Product);
            product.CompanyId = companyId;
            return _productDao.UpdateProduct(product);
        }

        public void CreateProductViewModel(ProductViewModel model, int companyId, int userId)
        {
            var product = ProductCast(model.Product);
            product.CompanyId = companyId;
            _productDao.CreateProduct(product, userId);
        }

        private Dictionary<int, string> CreateBrandDictionary(int companyId)
        {
            var brands = _brandDao.GetCompanyBrands(companyId);
            if (brands != null)
            {
                return brands.ToDictionary(b => b.Id, b => b.Name);
            }
            return new Dictionary<int, string>();
        }

        private static Product ProductCast(ProductModel model)
        {
            if (model != null)
            {
                return new Product()
                {
                    BrandId = model.BrandId,
                    Color = model.Color,
                    Description = model.Description,
                    Image = model.Image,
                    ImageType = model.ImageType,
                    Name = model.Name,
                    Id = model.Id,
                    Quantity = model.Quantity,
                    RowVersion = model.Version,
                    CompanyId = model.CompanyId
                };
            }
            return null;
        }

        private static ProductModel ProductModelCast(Product model)
        {
            if (model != null)
            {
                return new ProductModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    BrandId = model.BrandId,
                    Color = model.Color,
                    Description = model.Description,
                    Image = model.Image,
                    ImageType = model.ImageType,
                    Quantity = model.Quantity,
                    Version = model.RowVersion,
                    CompanyId = model.CompanyId
                };
            }
            return null;
        }

        public ProductViewModel GetLastProductViewMode(int companyId)
        {
            return new ProductViewModel()
            {
                Product = ProductModelCast(_productDao.GetLastProduct(companyId)),
                Brands = CreateBrandDictionary(companyId),
                Colors = ProductColors.Colors
            };
        }
    }
}
