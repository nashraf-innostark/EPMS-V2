using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IPhysicalCountRepository : IBaseRepository<PhysicalCount, long>
    {
        PhysicalCountResponse GetAllPhysicalCountResponse(PhysicalCountSearchRequest searchRequest);
    }
}
