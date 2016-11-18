using Autofac;
using SomeProducts.DAL.Models;
using SomeProducts.DAL.Repository;
using SomeProducts.DAL.Repository.Decorators;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.AutofacModule.RepositoryModules
{
    public class ProductModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository<Product>>().Named<IRepository<Product>>(nameof(Product));

            builder.RegisterDecorator<IRepository<Product>>(x => new ActiveDecorator<Product>(x),
                fromKey: nameof(Product))
                .Keyed<IRepository<Product>>(nameof(ActiveDecorator<Product>));
            builder.RegisterDecorator<IRepository<Product>>(x => new CompanyItemsAccessDecorator<Product>(x),
                fromKey: nameof(ActiveDecorator<Product>))
                .Keyed<IRepository<Product>>(nameof(CompanyItemsAccessDecorator<Product>));
            builder.RegisterDecorator<IRepository<Product>>(x => new DateModifiedDecorator<Product>(x),
                fromKey: nameof(CompanyItemsAccessDecorator<Product>));
        }
    }
}
