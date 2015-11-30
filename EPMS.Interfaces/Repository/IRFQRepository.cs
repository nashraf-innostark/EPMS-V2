using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;

namespace EPMS.Interfaces.Repository
{
    public interface IRFQRepository :IBaseRepository<RFQ, long>
    {
        RFQ FindByRfqId(long rfqId);
        IEnumerable<RFQ> GetAllRFQsByCustomerId(QOReportCreateOrDetailsRequest request);
        IEnumerable<RFQ> GetAllPendingRfqs();
        IEnumerable<RFQ> GetRfqsByCustomerId(long customerId);
        IEnumerable<RFQ> GetPendingRfqsByCustomerId(long customerId);
    }
}
