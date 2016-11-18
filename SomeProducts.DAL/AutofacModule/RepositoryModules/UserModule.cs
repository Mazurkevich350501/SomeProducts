using Autofac;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Decorators;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.AutofacModule.RepositoryModules
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository<User>>().Named<IRepository<User>>(nameof(User));
            builder.RegisterDecorator<IRepository<User>>(x => new ActiveDecorator<User>(x),
                fromKey: nameof(User)).Keyed<IRepository<User>>(nameof(ActiveDecorator<User>));
            builder.RegisterDecorator<IRepository<User>>(x => new CompanyItemsAccessDecorator<User>(x),
                fromKey: nameof(ActiveDecorator<User>))
                .Keyed<IRepository<User>>(nameof(CompanyItemsAccessDecorator<User>));
            builder.RegisterDecorator<IRepository<User>>(x => new AsyncDecorator<User>(x),
                fromKey: nameof(CompanyItemsAccessDecorator<User>));
        }
    }
}
