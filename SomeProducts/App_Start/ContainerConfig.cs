using Autofac;
using SomeProducts.Controllers;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.PresentationServices.IPresentationSevices.Create;
using SomeProducts.PresentationServices.IPresentationSevices.ProductTable;
using SomeProducts.PresentationServices.PresentaoinServices.Create;
using SomeProducts.PresentationServices.PresentaoinServices.ProductTable;

namespace SomeProducts
{
    public static class ContainerConfig
    { 
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BaseRepository<Product>>().As<IRepository<Product>>();
            builder.RegisterType<BaseRepository<Brand>>().As<IRepository<Brand>>();
            builder.RegisterType<ProductDao>().As<IProductDao>();
            builder.RegisterType<BrandDao>().As<IBrandDao>();
            builder.RegisterType<ProductViewModelPresentationService>().As<IProductViewModelPresentationService>();
            builder.RegisterType<BrandModelPresentationService>().As<IBrandModelPresentationService>();
            builder.RegisterType<ProductController>().As<ProductController>();
            builder.RegisterType<ProductTablePresentationService>().As<IProductTablePresentationService>();
            builder.RegisterType<ProductTableController>().As<ProductTableController>();
            return builder.Build();
        }
    }
}