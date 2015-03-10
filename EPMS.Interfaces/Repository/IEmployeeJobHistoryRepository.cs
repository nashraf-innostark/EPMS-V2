using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IEmployeeJobHistoryRepository : IBaseRepository<EmployeeJobHistory, long>
    {
        IEnumerable<EmployeeJobHistory> GetJobHistoryByEmployeeId(long empId);
    }
}
