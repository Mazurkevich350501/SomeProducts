
using System;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IDateModifiedRepository<out TEntity>
    {
        TEntity GetLast(int companyId);
    }
}
