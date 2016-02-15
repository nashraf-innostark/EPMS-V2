using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface ITaskEmployeeRepository : IBaseRepository<TaskEmployee, long>
    {
        /// <summary>
        /// Get task employee by employee id
        /// </summary>
        /// <param name="employeeId"></param>
        IEnumerable<TaskEmployee> GetTaskEmployeeByEmployeeId(long employeeId);
        /// <summary>
        /// Get no of tasks employee is working on
        /// </summary>
        /// <param name="id"></param>
        int CountTasksByEmployeeId(long id);
        /// <summary>
        /// Get Task Employees by Task Id to set IsDeleted true when task progress is full
        /// </summary>
        /// <param name="id"></param>
        IEnumerable<TaskEmployee> GetTaskEmployeeByTaskId(long id);
    }
}
