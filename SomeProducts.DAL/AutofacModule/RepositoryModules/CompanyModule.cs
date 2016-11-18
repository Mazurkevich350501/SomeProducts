using Autofac;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Decorators;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.AutofacModule.RepositoryModules
{
    public class CompanyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository<Company>>().Named<IRepository<Company>>(nameof(Company));

            builder.RegisterDecorator<IRepository<Company>>(x => new ActiveDecorator<Company>(x),
                fromKey: nameof(Company));
        }
    }
}
