using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRIFRepository : IBaseRepository<RIF, long>
    {
        RifRequestResponse LoadAllRifs(RifSearchRequest rifSearchRequest);
        IEnumerable<RIF> GetRecentRIFs(int status, string requester, DateTime date);
        string GetLastFormNumber();
    }
}
