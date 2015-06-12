using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IPoItemService
    {
        IEnumerable<PurchaseOrderItem> GetPoItemsByPoId(long id);
        PurchaseOrderItem Find(long id);
        bool AddPoItem(PurchaseOrderItem item);
        bool UpdatePoItem(PurchaseOrderItem item);
        void DeletePoItem(PurchaseOrderItem item);
    }
}
