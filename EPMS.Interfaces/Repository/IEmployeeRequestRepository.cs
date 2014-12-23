using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IEmployeeRequestRepository : IBaseRepository<EmployeeRequest, long>
    {
        IEnumerable<EmployeeRequest> GetAllRequests(long employeeId);
    }
}
