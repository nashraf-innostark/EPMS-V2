using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IQuotationRepository : IBaseRepository<Quotation, long>
    {
        QuotationResponse GetAllQuotation(QuotationSearchRequest searchRequest);
        Quotation FindQuotationByOrderId(long orderId);
        IEnumerable<Quotation> GetAllQuotationByCustomerId(long customerId);
        IEnumerable<Quotation> FindQuotationByIdForProjectDetail(long id);
    }
}
