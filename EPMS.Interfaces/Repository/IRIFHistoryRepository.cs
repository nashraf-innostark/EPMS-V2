using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRIFHistoryRepository : IBaseRepository<RIFHistory, long>
    {
        IEnumerable<RIFHistory> GetRifHistoryData(long parentId);
    }
}
