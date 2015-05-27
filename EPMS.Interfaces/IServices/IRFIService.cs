using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IRFIService
    {
        IEnumerable<RFI> GetAll();
        RFI FindRFIById(long id);
        bool SaveRFI(RFI rfi);
        bool AddRFI(RFI rfi);
        bool UpdateRFI(RFI rfi);
        void DeleteRFI(RFI rfi);
        RFIResponse LoadRfiResponseData(long? id);
    }
}
