using System;
using System.Collections.Generic;
using SomeProducts.DAL.IDao;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.Models.ProductTable;

namespace SomeProducts.PresentationServices.PresentaoinServices.ProductTable
{
    public class ProductTablePresentationService : IProductTablePresentationService
    {
        private readonly IProductDao _dao;

        public ProductTablePresentationService(IProductDao dao)
        {
            _dao = dao;
        }

        public ProductTableViewModel GetTablePage(int pageNumber, int productCount)
        {
            var model = new ProductTableViewModel {Products = new List<ProductTableModel>()};
            var productList = _dao.GetAllProducts();
            foreach (var product in productList)
            {
                model.Products.Add(new ProductTableModel()
                {
                    Brand = product.BrandId.ToString(),
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Description = product.Description,
                    Color = product.Color,
                    Image = product.Image,
                    ImageType = product.ImageType,
                    Id = product.Id
                });
            }
            return model;
        }
    }
}
