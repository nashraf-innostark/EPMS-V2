using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IRFIRepository : IBaseRepository<RFI, long>
    {
        RfiRequestResponse LoadAllRfis(RfiSearchRequest rfiSearchRequest);
    }
}
