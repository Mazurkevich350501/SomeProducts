﻿using Autofac;
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
            builder.RegisterType<BaseRepository<Product>>().As<IRepository<Product>>();
            builder.RegisterType<BaseRepository<Brand>>().As<IRepository<Brand>>();
            builder.RegisterType<ProductDao>().As<IProductDao>();
            builder.RegisterType<BrandDao>().As<IBrandDao>();
        }
    }
}