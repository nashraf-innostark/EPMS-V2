using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRFQRepository :IBaseRepository<RFQ, long>
    {
        RFQ FindByCustomerAndRfqId(long customerId, long rfqId);
    }
}
