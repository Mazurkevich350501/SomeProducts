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
            builder.RegisterType<BaseRepository<Product>>().As<IRepository<Product>>().SingleInstance();
            builder.RegisterType<BaseRepository<Brand>>().As<IRepository<Brand>>().SingleInstance();
            builder.RegisterType<ProductDao>().As<IProductDao>().SingleInstance();
            builder.RegisterType<BrandDao>().As<IBrandDao>().SingleInstance();
            builder.RegisterType<ProductViewModelPresentationService>().As<IProductViewModelPresentationService>().SingleInstance();
            builder.RegisterType<BrandModelPresentationService>().As<IBrandModelPresentationService>().SingleInstance();
            builder.RegisterType<ProductController>().As<ProductController>();
            return builder.Build();
        }
    }
}