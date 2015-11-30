using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;

namespace EPMS.Interfaces.Repository
{
    public interface IRFQRepository :IBaseRepository<RFQ, long>
    {
        RFQ FindByCustomerAndRfqId(long customerId, long rfqId);
        IEnumerable<RFQ> GetAllRFQsByCustomerId(QOReportCreateOrDetailsRequest request);
    }
}
