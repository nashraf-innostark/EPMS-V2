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
        QuotationResponse GetRfqForQuotationResponse(long rfqId);
        Quotation FindQuotationById(long id);
        IEnumerable<Quotation> FindQuotationByIdForProjectDetail(long id);
        Quotation FindQuotationByOrderId(long orderId);
        QuotationResponse AddQuotation(Quotation quotation);
        QuotationResponse UpdateQuotation(Quotation quotation);
        void DeleteQuotation(Quotation quotation);
    }
}
