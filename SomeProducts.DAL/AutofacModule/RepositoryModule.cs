using Autofac;
using SomeProducts.DAL.AutofacModule.RepositoryModules;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.AutofacModule
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository<AuditItem>>().As<IRepository<AuditItem>>();
            builder.RegisterModule<BrandModule>();
            builder.RegisterModule<ProductModule>();
            builder.RegisterModule<CompanyModule>();
            builder.RegisterModule<UserModule>();
            builder.RegisterModule<RoleModule>();
        }
    }
}
