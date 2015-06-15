using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IDIFHistoryRepository : IBaseRepository<DIFHistory, long>
    {
        IEnumerable<DIFHistory> GetDifHistoryData();
    }
}
