
using System;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IDateModifiedRepository<TEntity> : IRepository<TEntity>
    {
        TEntity GetLast();
        DateTime GetCreateTime(int id);
    }
}
