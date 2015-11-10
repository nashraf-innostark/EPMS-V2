using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProjectTaskService
    {
        ProjectTask FindProjectTaskById(long id);
        IEnumerable<ProjectTask> FindProjectTaskByProjectId(long projectid, long taskId);
        IEnumerable<ProjectTask> FindParentTasksByProjectId(long projectid);
        IEnumerable<ProjectTaskResponse> LoadProjectTasksByEmployeeId(long employeeId, long projectId);
        IEnumerable<ProjectTask> GetAll();
        IEnumerable<ProjectTask> GetTasksByProjectId(long projectId);
        bool AddProjectTask(ProjectTask task, List<long> preReqList, List<long> assignedEmployee);
        bool UpdateProjectTask(ProjectTask task, List<long> dbTaskList, List<long> clientTaskList, List<long> dbEmployeeList, List<long> clientEmployeeList);
        void DeleteProjectTask(long taskId);
        TaskResponse GetAllTasks(TaskSearchRequest searchRequest);
        TaskResponse GetProjectTasksForEmployee(TaskSearchRequest searchRequest, long employeeId);
        TaskResponse GetProjectTasksForCustomer(TaskSearchRequest searchRequest, long customerId);
        TaskResponse GetResponseForAddEdit(long id);
        CreateTaskReportsResponse GetAllProjectsAndTasks();
    }
}
