using System.Linq;
using PagedList;
using SomeProducts.DAL.IDao;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;
using SomeProducts.DAL.Models;

namespace SomeProducts.PresentationServices.PresentaoinServices.ProductTable
{
    public class ProductTablePresentationService : IProductTablePresentationService
    {
        private readonly IProductDao _dao;

        public ProductTablePresentationService(IProductDao dao)
        {
            _dao = dao;
        }

        public ProductTableViewModel GetTablePage(PageInfo info)
        {
            var model = new ProductTableViewModel();
            var productList = _dao.GetSortedProducts(info.SortingOption);
            var tableList = productList.Select(ProductTableModelCast).ToList();
            model.Products = tableList.ToPagedList(info.Page, info.ProductCount);
            model.PageInfo = info;
            return model;
        }


        private ProductTableModel ProductTableModelCast(Product product)
        {
            if (product == null) return null;
            return new ProductTableModel()
            {
                Brand = product.BrandId.ToString(),
                Name = product.Name,
                Quantity = product.Quantity,
                Description = product.Description,
                Color = product.Color,
                Image = product.Image,
                ImageType = product.ImageType,
                Id = product.Id
            };
        }
    }
}
