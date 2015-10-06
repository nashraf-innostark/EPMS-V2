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
        IEnumerable<Quotation> GetAllQuotationByCustomerId(long customerId);
        QuotationResponse GetQuotationResponse(long quotationId, long customerId, string from);
        QuotationResponse GetQuotationResponseForRfq(long customerId, long rfqId);
        Quotation FindQuotationById(long id);
        IEnumerable<Quotation> FindQuotationByIdForProjectDetail(long id);
        Quotation FindQuotationByOrderId(long orderId);
        long AddQuotation(Quotation quotation);
        bool UpdateQuotation(Quotation quotation);
        void DeleteQuotation(Quotation quotation);
    }
}
