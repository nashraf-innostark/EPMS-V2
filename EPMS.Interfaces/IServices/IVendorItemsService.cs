using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IVendorItemsService
    {
        IEnumerable<VendorItem> GetAll();
        VendorItem FindItemsById(long id);

        bool AddItem(VendorItem vendor);
        bool UpdateItem(VendorItem vendor);
        void DeleteItem(VendorItem vendor);
        IEnumerable<VendorItem> GetItemsByVendorId(long vendorId);


    }
}
