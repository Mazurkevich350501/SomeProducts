
using System;

namespace SomeProducts.DAL.Repository.Interface
{
    public interface IDateModifiedRepository<TEntity> : IRepository<TEntity>, ICompanyItemsAccess<TEntity>
    {
        TEntity GetLast(int companyId);
    }
}
