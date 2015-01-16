using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IQuotationItemService
    {
        IEnumerable<QuotationItemDetail> GetAll();
        QuotationItemDetail FindQuotationById(long id);
        bool AddQuotationItem(QuotationItemDetail itemDetail);
        bool UpdateQuotationItem(QuotationItemDetail itemDetail);
        void DeleteQuotationItem(QuotationItemDetail itemDetail);
    }
}
