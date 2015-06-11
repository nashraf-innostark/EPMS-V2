using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IDIFService
    {
        IEnumerable<DIF> GetAll();
        DifRequestResponse LoadAllDifs(DifSearchRequest searchRequest);
        DifHistoryResponse GetDifHistoryData();
        DIF FindDIFById(long id);
        bool SaveDIF(DIF rif);
        bool AddDIF(DIF rif);
        bool UpdateDIF(DIF rif);
        void DeleteDIF(DIF rif);
        DifCreateResponse LoadDifResponseData(long? id);
    }
}
