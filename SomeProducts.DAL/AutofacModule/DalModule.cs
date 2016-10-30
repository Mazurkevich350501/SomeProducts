using Autofac;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.AutofacModule
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductContext>().As<ProductContext>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<BaseRepositoryAsync<Role>>().As<IRepositoryAsync<Role>>();

            builder.RegisterType<BaseRepository<Company>>().As<IRepository<Company>>();
            builder.RegisterType<DateModifiedRepository<Product>>().As<IDateModifiedRepository<Product>>();
            builder.RegisterType<DateModifiedRepository<Brand>>().As<IDateModifiedRepository<Brand>>();

            builder.RegisterType<CompanyDao>().As<ICompanyDao>();
            builder.RegisterType<ProductDao>().As<IProductDao>();
            builder.RegisterType<BrandDao>().As<IBrandDao>();
            builder.RegisterType<UserDao>().As<IUserStore<User, int>>();
            builder.RegisterType<UserDao>().As<IUserDao>();
            builder.RegisterType<RoleDao>().As<IRoleStore<Role, int>>();
        }
    }
}
