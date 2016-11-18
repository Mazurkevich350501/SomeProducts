using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using SomeProducts.DAL.Context;
using SomeProducts.DAL.Dao;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Decorators;

namespace SomeProducts.DAL.AutofacModule
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductContext>().As<ProductContext>().InstancePerHttpRequest();

            builder.RegisterType<AuditDao>().As<IAuditDao>();
            builder.RegisterType<CompanyDao>().As<ICompanyDao>();
            builder.RegisterType<ProductDao>().As<IProductDao>();
            builder.RegisterType<BrandDao>().As<IBrandDao>();
            builder.RegisterType<UserDao>().As<IUserDao>();
            builder.RegisterType<UserDao>().As<IUserStore<User, int>>();
            builder.RegisterType<RoleDao>().As<IRoleStore<Role, int>>();
            
            builder.RegisterModule<RepositoryModule>();
        }
    }
}
