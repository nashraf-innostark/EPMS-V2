using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRIFItemRepository:IBaseRepository<RIFItem,long>
    {
        IEnumerable<RIFItem> GetRifItemsByRifId(long rifId);
    }
}
