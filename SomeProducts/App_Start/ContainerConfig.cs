using Autofac;
using SomeProducts.Controllers;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.PresentationServices.IPresentationSevices;
using SomeProducts.PresentationServices.PresentaoinServices;

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
            return builder.Build();
        }
    }
}