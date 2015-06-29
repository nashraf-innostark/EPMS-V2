using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IPurchaseOrderService
    {
        POCreateResponse GetPoResponseData(long? id);
        PODetailResponse GetPoDetailResponse(long id, string from);
        PoHistoryResponse GetPoHistoryData(long? parentId);
        PurchaseOrder FindPoById(long id, string from);
        PurchaseOrderListResponse GetAllPoS(PurchaseOrderSearchRequest searchRequest);
        bool SavePO(PurchaseOrder purchaseOrder);
        bool AddPO(PurchaseOrder purchaseOrder);
        bool UpdatePO(PurchaseOrder purchaseOrder);
        bool UpdatePOStatus(PurchaseOrderStatus purchaseOrder);
        void DeletePO(PurchaseOrder purchaseOrder);
        IEnumerable<PurchaseOrder> GetRecentPOs(int status, string requester, DateTime date);
    }
}
