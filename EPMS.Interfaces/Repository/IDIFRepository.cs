using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IDIFRepository : IBaseRepository<DIF, long>
    {
        DifRequestResponse LoadAllDifs(DifSearchRequest searchRequest);
        IEnumerable<DIF> GetDifHistoryData();
    }
}
