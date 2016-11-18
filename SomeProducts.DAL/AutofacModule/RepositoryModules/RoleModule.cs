using Autofac;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Decorators;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.AutofacModule.RepositoryModules
{
    public class RoleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository<Role>>().Named<IRepository<Role>>(nameof(Role));

            builder.RegisterDecorator<IRepository<Role>>(x => new AsyncDecorator<Role>(x),
                fromKey: nameof(Role));
        }
    }
}
