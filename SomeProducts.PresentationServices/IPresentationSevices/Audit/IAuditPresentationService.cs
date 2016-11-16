
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.Models;
using SomeProducts.PresentationServices.Models.Audit;

namespace SomeProducts.PresentationServices.IPresentationSevices.Audit
{
    public interface IAuditPresentationService
    {
        AuditViewTableForEntity GetAuditViewTableForItem(
            PageInfo pageInfo,
            string entity,
            int entityId);

        AuditViewTable GetFullAuditViewTable(
           PageInfo pageInfo,
           FilterInfo filterInfo);
    }
}
