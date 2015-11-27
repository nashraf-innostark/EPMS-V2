using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;

namespace EPMS.Interfaces.Repository
{
    public interface IInventoryItemRepository : IBaseRepository<InventoryItem, long>
    {
        bool ItemExists(InventoryItem item);
        IEnumerable<InventoryItem> GetInventoryItemReportDetails(InventoryItemReportCreateOrDetailsRequest request);
    }
}
