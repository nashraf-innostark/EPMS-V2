using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder, long>
    {
        PurchaseOrderListResponse GetAllPoS(PurchaseOrderSearchRequest searchRequest);
        IEnumerable<PurchaseOrder> GetRecentPOs(int status, string requester, DateTime date);
        IEnumerable<PurchaseOrder> FindPoByVendorId(long vendorId);
        string GetLastFormNumber();
    }
}
