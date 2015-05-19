using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IVendorItemsRepository : IBaseRepository<VendorItem, long>
    {
        IEnumerable<VendorItem> GetItemsByVendorId(long vendorId); 

    }
}
