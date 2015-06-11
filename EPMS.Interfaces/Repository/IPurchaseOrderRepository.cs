using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder, long>
    {
        PurchaseOrderListResponse GetAllPoS(PurchaseOrderSearchRequest searchRequest);
    }
}
