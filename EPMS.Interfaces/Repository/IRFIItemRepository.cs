using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRFIItemRepository:IBaseRepository<RFIItem,long>
    {
        IEnumerable<RFIItem> GetRfiItemsByRfiId(long rfiId);
    }
}
