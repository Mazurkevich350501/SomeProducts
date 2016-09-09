using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SomeProducts.Controllers;
using SomeProducts.PresentationServices.IPresentationSevices;
using SomeProducts.PresentationServices.Models;

namespace SomeProducts.Test
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Create_Should_Return_CreateView_And_Empty_ProductViewModel()
        {
            var brandModelService = new Mock<IBrandModelPresentationService>();
            var producrModelService = new Mock<IProductViewModelPresentationService>();
            var product = new ProductViewModel
            {
                Product = new ProductModel(),
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            producrModelService.Setup(p => p.GetProductViewModel(It.IsAny<int?>())).Returns(product);
            var controller = new ProductController(producrModelService.Object, brandModelService.Object);

            var result = controller.Edit(1) as ViewResult;

            if (result != null)
            {
                Assert.AreEqual("Create", result.ViewName);
                Assert.AreEqual(product, result.Model);
            }
        }

        [TestMethod]
        public void Create_Should_Redirect_And_Save_If_ProductModel_Is_Valid()
        {
            var brandModelService = new Mock<IBrandModelPresentationService>();
            var producrModelService = new Mock<IProductViewModelPresentationService>();
            var product = new ProductViewModel
            {
                Product = new ProductModel()
                {
                    BrandId = 5,
                    ProductId = 5,
                    Name = "name",
                    Quantity = 5
                },
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            producrModelService.Setup(s => s.GetLastProductViewMode()).Returns(product);
            var controller = new ProductController(producrModelService.Object, brandModelService.Object);

            var result = controller.Create(product) as RedirectToRouteResult;

            if (result != null)
            {
                Assert.AreEqual("Product", result.RouteValues["controller"]);
                Assert.AreEqual("Edit", result.RouteValues["action"]);
                Assert.AreEqual(product.Product.ProductId, result.RouteValues["id"]);
            }
        }

        [TestMethod]
        public void Create_Should_Not_Redirect_And_Save_If_ProductModel_Is_Not_Valid()
        {
            var brandModelService = new Mock<IBrandModelPresentationService>();
            var producrModelService = new Mock<IProductViewModelPresentationService>();
            var product = new ProductViewModel
            {
                Product = new ProductModel()
                {
                    Description = "description",
                    ImageType = "imageType"
                },
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            producrModelService.Setup(s => s.GetProductViewModel(It.IsAny<int?>())).Returns(product);
            var controller = new ProductController(producrModelService.Object, brandModelService.Object);

            var result = controller.Create(null) as ViewResult;

            if (result != null)
            {
                Assert.AreEqual(product, result.Model);
                Assert.AreEqual("Create", result.ViewName);
            }
        }

        [TestMethod]
        public void Edit_Should_Redirect_To_ErrorView_If_Id_Is_Not_Correct()
        {
            var brandModelService = new Mock<IBrandModelPresentationService>();
            var producrModelService = new Mock<IProductViewModelPresentationService>();
            producrModelService.Setup(p => p.GetProductViewModel(It.IsAny<int?>())).Returns(null as ProductViewModel);
            var controller = new ProductController(producrModelService.Object, brandModelService.Object);

            var result = controller.Edit(5) as ViewResult;

            if (result != null)
            {
                Assert.AreEqual("Error", result.ViewName);
            }
        }

        [TestMethod]
        public void Edit_Should_Return_EditView()
        {
            var brandModelService = new Mock<IBrandModelPresentationService>();
            var producrModelService = new Mock<IProductViewModelPresentationService>();
            const int id = 5;
            var product = new ProductViewModel
            {
                Product = new ProductModel() { ProductId = id },
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            producrModelService.Setup(p => p.GetProductViewModel(It.IsAny<int?>())).Returns(product);
            var controller = new ProductController(producrModelService.Object, brandModelService.Object);

            var result = controller.Edit(id) as ViewResult;

            if (result != null)
            {
                Assert.AreEqual("Create", result.ViewName);
                Assert.AreEqual(product, result.Model);
            }
        }

        [TestMethod]
        public void Edit_Save_Should_Return_EditView_And_Saved_ProductViewModel()
        {
            var brandModelService = new Mock<IBrandModelPresentationService>();
            var producrModelService = new Mock<IProductViewModelPresentationService>();
            var product = new ProductViewModel { Product = new ProductModel() { ProductId = 5 } };
            var newProduct = new ProductViewModel()
            {
                Product = new ProductModel(),
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            producrModelService.Setup(p => p.GetProductViewModel(It.IsAny<int?>())).Returns(newProduct);
            var controller = new ProductController(producrModelService.Object, brandModelService.Object);

            var result = controller.Edit(product) as ViewResult;

            if (result != null)
            {
                Assert.AreEqual("Create", result.ViewName);
                Assert.AreEqual(newProduct, result.Model);
            }
        }
    }
}
