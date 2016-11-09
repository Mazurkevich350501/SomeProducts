﻿
using SomeProducts.CrossCutting.Filter.Model;
using SomeProducts.PresentationServices.Models;
using SomeProducts.PresentationServices.Models.Audit;

namespace SomeProducts.PresentationServices.IPresentationSevices.Audit
{
    public interface IAuditPresentationService
    {
        AuditViewTable GetAuditViewTableForItem(
            PageInfo pageInfo,
            string entity,
            string value, 
            int companyId);

        AuditViewTable GetFullAuditViewTable(
           PageInfo pageInfo,
           FilterInfo filterInfo,
           int? companyId);
    }
}