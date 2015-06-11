using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IPoItemRepository : IBaseRepository<PurchaseOrderItem, long>
    {
        IEnumerable<PurchaseOrderItem> GetPoItemsByPoId(long id);
    }
}
