using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IPhysicalCountItemRepository : IBaseRepository<PhysicalCountItem, long>
    {
        IEnumerable<PhysicalCountItem> GetPCiItemsByPCiId(long pcId);
    }
}
