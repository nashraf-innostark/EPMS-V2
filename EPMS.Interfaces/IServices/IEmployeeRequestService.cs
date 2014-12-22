using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IEmployeeRequestService
    {
        long AddRequest(EmployeeRequest model);
        long AddRequestDetail(RequestDetail model);
        EmployeeRequest Find(long id);
        RequestDetail GetRequestDetailByRequestId(long requestId);
        bool UpdateRequest(EmployeeRequest request);
        bool UpdateRequestDetail(RequestDetail requestDetail);
    }
}
