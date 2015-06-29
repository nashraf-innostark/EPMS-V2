using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface ITIRRepository : IBaseRepository<TIR, long>
    {
        TIRListResponse GetAllTirs(TransferItemSearchRequest searchRequest);
        IEnumerable<TIR> GetRecentTIRs(int status, string requester, DateTime date);
    }
}
