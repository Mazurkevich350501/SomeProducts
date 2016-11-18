using Autofac;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Decorators;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.AutofacModule.RepositoryModules
{
    public class BrandModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository<Brand>>().Named<IRepository<Brand>>(nameof(Brand));

            builder.RegisterDecorator<IRepository<Brand>>(x => new ActiveDecorator<Brand>(x),
                fromKey: nameof(Brand))
                .Keyed<IRepository<Brand>>(nameof(ActiveDecorator<Brand>));
            builder.RegisterDecorator<IRepository<Brand>>(x => new CompanyItemsAccessDecorator<Brand>(x),
                fromKey: nameof(ActiveDecorator<Brand>))
                .Keyed<IRepository<Brand>>(nameof(CompanyItemsAccessDecorator<Brand>));
            builder.RegisterDecorator<IRepository<Brand>>(x => new DateModifiedDecorator<Brand>(x),
                fromKey: nameof(CompanyItemsAccessDecorator<Brand>));
        }
    }
}
