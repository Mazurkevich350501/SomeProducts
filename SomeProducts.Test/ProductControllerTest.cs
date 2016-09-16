using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;
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
        private Mock<IBrandModelPresentationService> _brandModelService;
        private Mock<IProductViewModelPresentationService> _productModelService;
        private ProductController _controller;
        private List<BrandModel> _allBrands;

        [TestInitialize]
        public void TestInitialize()
        {
            _productModelService = new Mock<IProductViewModelPresentationService>();
            _brandModelService = new Mock<IBrandModelPresentationService>();
            _controller = new ProductController(_productModelService.Object, _brandModelService.Object);

            _allBrands = new List<BrandModel>
            {
                new BrandModel() {Id = 1, Name = "name1"},
                new BrandModel() {Id = 2, Name = "name2"}
            };
        }

        [TestMethod]
        public void Create_Should_Return_CreateView_And_Empty_ProductViewModel()
        {
            var product = new ProductViewModel
            {
                Product = new ProductModel(),
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            _productModelService.Setup(p => p.GetProductViewModel(It.IsAny<int?>())).Returns(product);

            var result = _controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewName.IsEmpty());
            Assert.AreEqual(product, result.Model);
        }

        [TestMethod]
        public void Create_Should_Redirect_And_Save_If_ProductModel_Is_Valid()
        {
            var product = new ProductViewModel
            {
                Product = new ProductModel()
                {
                    BrandId = 5,
                    Id = 5,
                    Name = "name",
                    Quantity = 5
                },
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            _productModelService.Setup(s => s.GetLastProductViewMode()).Returns(product);

            var result = _controller.Create(product) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Product", result.RouteValues["controller"]);
            Assert.AreEqual("Edit", result.RouteValues["action"]);
            Assert.AreEqual(product.Product.Id, result.RouteValues["id"]);
        }

        [TestMethod]
        public void Create_Should_Not_Redirect_And_Save_If_ProductModel_Is_Not_Valid()
        {
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
            _productModelService.Setup(s => s.GetProductViewModel(It.IsAny<int?>())).Returns(product);
            _controller.ModelState.AddModelError("Id", "Product Id is not valid.");

            var result = _controller.Create(product) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(product, result.Model);
            Assert.IsTrue(result.ViewName.IsEmpty());
        }

        [TestMethod]
        public void Edit_Should_Redirect_To_ErrorView_If_Id_Is_Not_Correct()
        {
            _productModelService.Setup(p => p.GetProductViewModel(It.IsAny<int?>())).Returns(new ProductViewModel());

            var result = _controller.Edit(5) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void Edit_Should_Return_EditView()
        {
            const int id = 5;
            var product = new ProductViewModel
            {
                Product = new ProductModel() { Id = id },
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            _productModelService.Setup(p => p.GetProductViewModel(It.IsAny<int?>())).Returns(product);

            var result = _controller.Edit(id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
            Assert.AreEqual(product, result.Model);
        }

        [TestMethod]
        public void Edit_Save_Should_Return_EditView_If_ProductViewModel_Is_Not_Valid()
        {

            var product = new ProductViewModel { Product = new ProductModel() { Id = 5 } };
            var newProduct = new ProductViewModel()
            {
                Product = new ProductModel(),
                Colors = new ProductColors().Colors,
                Brands = new Dictionary<int, string>()
            };
            _controller.ModelState.AddModelError("Name", "Product Id is not valid.");
            _productModelService.Setup(p => p.GetProductViewModel(It.IsAny<int?>())).Returns(newProduct);

            var result = _controller.Edit(product) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
            Assert.AreEqual(newProduct, result.Model);

        }

        [TestMethod]
        public void Edit_Save_Should_Redirect_To_Edit_If_ProductViewModel_Is_Valid()
        {
            var product = new ProductViewModel { Product = new ProductModel() { Id = 5 } };
            _productModelService.Setup(s => s.UpdateProductViewModel(It.IsAny<ProductViewModel>())).Returns(true);

            var result = _controller.Edit(product) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.RouteValues["Action"]);
            Assert.AreEqual("Product", result.RouteValues["Controller"]);
            Assert.AreEqual(product.Product.Id, result.RouteValues["id"]);
        }

        [TestMethod]
        public void Edit_Save_Should_Redirect_To_ErrorView_If_Product_Alredy_Has_Change()
        {
            _productModelService.Setup(p => p.UpdateProductViewModel(It.IsAny<ProductViewModel>())).Returns(false);

            var result = _controller.Edit(new ProductViewModel()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void Delete_Should_Return_Create_Url_And_Call_RemoveViewBrandModel()
        {
            const string testUrl = "testUrl/testUrl";
            var isCalledRemoveProductViewModel = false;
            var urlHelperMock = new Mock<UrlHelper>();
            _controller.Url = urlHelperMock.Object;
            urlHelperMock.Setup(x => x.Action("Create", "Product")).Returns(testUrl);
            _productModelService.Setup(s => s.RemoveProductViewModel(It.IsAny<int>()))
                .Callback(() => { isCalledRemoveProductViewModel = true; });

            var result = _controller.Delete(1);

            Assert.AreEqual(testUrl, result.Data);
            Assert.IsTrue(isCalledRemoveProductViewModel);
        }

        [TestMethod]
        public void IsBrandUsing_Should_True_If_Brand_Is_Using()
        {
            _brandModelService.Setup(s => s.IsBrandModelUsing(It.IsAny<int>())).Returns(true);

            var result = _controller.IsBrandUsing(5);

            Assert.AreEqual(true, result.Data);
        }

        [TestMethod]
        public void GetBrandsList_Should_Return_All_Brands()
        {
            _brandModelService.Setup(s => s.GetAllItems()).Returns(_allBrands);

            var result = _controller.GetBrandsList();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_allBrands, (List<BrandModel>)result.Data);
        }

        [TestMethod]
        public void SaveBrandChanges_Should_Call_SaveBrandChanges_And_Return_BrandList()
        {
            var isCalledSaveBrandChanges = false;
            _brandModelService.Setup(s => s.SaveBrandChanges(It.IsAny<BrandsChangeModel>()))
                .Callback(() => isCalledSaveBrandChanges = true);
            _brandModelService.Setup(s => s.GetAllItems()).Returns(_allBrands);

            var result = _controller.SaveBrandsChanges(null);

            Assert.IsTrue(isCalledSaveBrandChanges);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_allBrands, (List<BrandModel>)result.Data);
        }
    }
}
