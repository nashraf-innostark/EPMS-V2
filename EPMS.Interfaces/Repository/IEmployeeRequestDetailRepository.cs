using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IEmployeeRequestDetailRepository : IBaseRepository<RequestDetail, long>
    {
        RequestDetail LoadRequestDetailByRequestId(long requestId);
    }
}
