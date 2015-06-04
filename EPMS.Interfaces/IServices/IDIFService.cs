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
        DIF FindRIFById(long id);
        bool SaveRIF(DIF rif);
        bool AddRIF(DIF rif);
        bool UpdateRIF(DIF rif);
        void DeleteRIF(DIF rif);
        DifCreateResponse LoadDifResponseData(long? id);
    }
}
