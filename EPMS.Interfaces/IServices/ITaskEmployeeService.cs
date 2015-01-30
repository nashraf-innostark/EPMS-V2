using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface ITaskEmployeeService
    {
        TaskEmployee FindTaskEmployeeById(long id);
        int CountTasksByEmployeeId(long id);
        IEnumerable<TaskEmployee> GetAll();
        IEnumerable<TaskEmployee> GetTaskEmployeeByEmployeeId(long employeeId);
        bool AddTaskEmployee(TaskEmployee employee);
        bool UpdateTaskEmployee(TaskEmployee employee);
        void DeleteTaskEmployee(TaskEmployee employee);
    }
}
