using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IRFIService
    {
        IEnumerable<RFI> GetAll();
        RFI FindRFIById(long id);
        bool AddRFI(RFI rfi);
        bool UpdateRFI(RFI rfi);
        void DeleteRFI(RFI rfi);
    }
}
