using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IDIFItemRepository:IBaseRepository<DIFItem,long>
    {
        IEnumerable<DIFItem> GetRfiItemsByRfiId(long rfiId);
    }
}
