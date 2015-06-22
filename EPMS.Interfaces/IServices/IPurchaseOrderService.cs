using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IPurchaseOrderService
    {
        POCreateResponse GetPoResponseData(long? id);
        PurchaseOrder FindPoById(long id, string from);
        PurchaseOrderListResponse GetAllPoS(PurchaseOrderSearchRequest searchRequest);
        bool SavePO(PurchaseOrder purchaseOrder);
        bool AddPO(PurchaseOrder purchaseOrder);
        bool UpdatePO(PurchaseOrder purchaseOrder);
        void DeletePO(PurchaseOrder purchaseOrder);
    }
}
