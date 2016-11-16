using System.Linq;
using SomeProducts.DAL.Models.Audit;

namespace SomeProducts.DAL.IDao
{
    public interface IAuditDao
    {
        AuditItem GetItemByid(int id);

        IQueryable<AuditItem> GetAllItems();

        int CreateEditAuditItems<T>(T previousObject, T nextObject);

        void CreateCreateAuditItem<T>(T createdObject);

        void CreateDeleteAuditItem<T>(T removingObject);

        IQueryable<AuditItem> GetCompanyItems(int companyId);
    }
}