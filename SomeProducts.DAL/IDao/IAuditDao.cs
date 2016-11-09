using System.Linq;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.IDao
{
    public interface IAuditDao
    {
        AuditItem GetItemByid(int id);

        IQueryable<AuditItem> GetAllItems();

        int CreateEditAuditItems<T>(T previousObject, T nextObject, int userId);

        void CreateCreateAuditItem<T>(T createdObject, int userId);

        void CreateDeleteAuditItem<T>(T removingObject, int userId);

        IQueryable<AuditItem> GetCompanyItems(int companyId);
    }
}