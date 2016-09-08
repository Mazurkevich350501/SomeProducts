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
            builder.RegisterType<BaseRepository<Product>>().As<IRepository<Product>>().InstancePerRequest();
            builder.RegisterType<BaseRepository<Brand>>().As<IRepository<Brand>>().InstancePerRequest();
            builder.RegisterType<ProductDao>().As<IProductDao>().InstancePerRequest();
            builder.RegisterType<BrandDao>().As<IBrandDao>().InstancePerRequest();
            builder.RegisterType<ProductViewModelPresentationService>().As<IProductViewModelPresentationService>().InstancePerRequest();
            builder.RegisterType<BrandModelPresentationService>().As<IBrandModelPresentationService>().InstancePerRequest();
            builder.RegisterType<ProductController>().As<ProductController>().InstancePerRequest();
            return builder.Build();
        }
    }
}