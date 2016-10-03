using Autofac;
using Microsoft.AspNet.Identity;
using SomeProducts.Controllers;
using SomeProducts.DAL.Context;
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
            builder.RegisterType<ProductContext>().As<ProductContext>();
            builder.RegisterType<UserRepository>().As<IRepository<User>>();
            builder.RegisterType<UserDao>().As<IUserStore<User, int>>();
            builder.RegisterType<AccountManager>().As<AccountManager>();
            builder.RegisterType<UserPresentationService>().As<UserPresentationService>();
            builder.RegisterType<AccountController>().As<AccountController>();

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