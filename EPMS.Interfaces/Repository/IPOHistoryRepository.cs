using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IPOHistoryRepository : IBaseRepository<PurchaseOrderHistory, long>
    {
        IEnumerable<PurchaseOrderHistory> GetPoHistoryData(long parentId);
    }
}
