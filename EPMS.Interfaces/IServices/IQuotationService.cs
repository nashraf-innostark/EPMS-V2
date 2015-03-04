using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IQuotationService
    {
        QuotationResponse GetAllQuotation(QuotationSearchRequest searchRequest);
        IEnumerable<Quotation> GetAll();
        Quotation FindQuotationById(long id);
        Quotation FindQuotationByOrderId(long orderId);
        long AddQuotation(Quotation quotation);
        bool UpdateQuotation(Quotation quotation);
        void DeleteQuotation(Quotation quotation);
    }
}
