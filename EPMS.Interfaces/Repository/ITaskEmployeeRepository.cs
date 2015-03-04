using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface ITaskEmployeeRepository : IBaseRepository<TaskEmployee, long>
    {
        IEnumerable<TaskEmployee> GetTaskEmployeeByEmployeeId(long employeeId);
        int CountTasksByEmployeeId(long id);
    }
}
