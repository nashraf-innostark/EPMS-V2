using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRFIHistoryRepository : IBaseRepository<RFIHistory, long>
    {
        IEnumerable<RFIHistory> GetRfiHistoryData();
    }
}
