using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IEmployeeRequestRepository : IBaseRepository<EmployeeRequest, long>
    {
        IEnumerable<EmployeeRequest> GetAllRequests(long employeeId);
        EmployeeRequestResponse GetAllRequests(EmployeeRequestSearchRequest searchRequset);
        IEnumerable<EmployeeRequest> GetAllMonetaryRequests();
    }
}
