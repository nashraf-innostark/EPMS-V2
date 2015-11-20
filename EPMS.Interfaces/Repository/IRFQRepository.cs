using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRFQRepository :IBaseRepository<RFQ, long>
    {
        RFQ FindByRfqId(long rfqId);
        IEnumerable<RFQ> GetAllPendingRfqs();
        IEnumerable<RFQ> GetPendingRfqsByCustomerId(long customerId);
    }
}
