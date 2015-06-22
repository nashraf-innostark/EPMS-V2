using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface ITIRHistoryRepository : IBaseRepository<TIRHistory, long>
    {
        IEnumerable<TIRHistory> GetTirHistoryData(long parentId);
    }
}
