using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IRIFService
    {
        IEnumerable<RIF> GetAll();
        RifRequestResponse LoadAllRifs(RifSearchRequest searchRequest);
        RifHistoryResponse GetRifHistoryData(long? parentId);
        RIF FindRIFById(long id);
        bool SaveRIF(RIF rif);
        bool AddRIF(RIF rif);
        bool UpdateRIF(RIF rif);
        void DeleteRIF(RIF rif);
        RifCreateResponse LoadRifResponseData(long? id, bool loadCustomersAndOrders, string from);
    }
}
