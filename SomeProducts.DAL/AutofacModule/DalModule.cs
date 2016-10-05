using Autofac;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;

namespace SomeProducts.DAL.AutofacModule
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductContext>().As<ProductContext>();
            builder.RegisterType<UserRepository>().As<IRepository<User>>();

            builder.RegisterType<BaseRepository<Product>>().As<IRepository<Product>>();
            builder.RegisterType<BaseRepository<Brand>>().As<IRepository<Brand>>();

            builder.RegisterType<ProductDao>().As<IProductDao>();
            builder.RegisterType<BrandDao>().As<IBrandDao>();
            builder.RegisterType<UserDao>().As<IUserStore<User, int>>();
        }
    }
}
