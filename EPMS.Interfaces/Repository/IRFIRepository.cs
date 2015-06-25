using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRFIRepository : IBaseRepository<RFI, long>
    {
        RfiRequestResponse LoadAllRfis(RfiSearchRequest rfiSearchRequest);
        IEnumerable<RFI> GetRecentRFIs(int status, string requester, DateTime? date);
    }
}
