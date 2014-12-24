using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IEmployeeRequestService
    {
        long AddRequest(EmployeeRequest model);
        long AddRequestDetail(RequestDetail model);
        EmployeeRequest Find(long id);
        EmployeeRequestResponse LoadAllRequests(EmployeeRequestSearchRequest searchRequset);
        IEnumerable<EmployeeRequest> LoadAllRequests(string requester);
        RequestDetail LoadRequestDetailByRequestId(long requestId);
        bool UpdateRequest(EmployeeRequest request);
        bool UpdateRequestDetail(RequestDetail requestDetail);
    }
}
