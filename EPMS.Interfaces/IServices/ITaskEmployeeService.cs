using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface ITaskEmployeeService
    {
        TaskEmployee FindTaskEmployeeById(long id);
        IEnumerable<TaskEmployee> GetTaskEmployeeByTaskId(long id);
        int CountTasksByEmployeeId(long id);
        IEnumerable<TaskEmployee> GetAll();
        IEnumerable<TaskEmployee> GetTaskEmployeeByEmployeeId(long employeeId);
        bool AddTaskEmployee(TaskEmployee employee);
        bool UpdateTaskEmployee(TaskEmployee employee);
        bool UpdateTaskEmployeeWithOutNotification(TaskEmployee employee);
        void DeleteTaskEmployee(TaskEmployee employee);
    }
}
