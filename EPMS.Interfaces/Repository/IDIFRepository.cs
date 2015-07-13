using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IDIFRepository : IBaseRepository<DIF, long>
    {
        DifRequestResponse LoadAllDifs(DifSearchRequest searchRequest);
        DIF Find(long id);
        IEnumerable<DIF> GetRecentDIFs(int status, string requester, DateTime date);
        string GetLastFormNumber();
    }
}
