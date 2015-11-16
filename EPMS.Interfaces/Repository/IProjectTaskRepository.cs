using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProjectTaskRepository : IBaseRepository<ProjectTask, long>
    {
        IEnumerable<ProjectTask> GetTasksByProjectId(long projectId);
        IEnumerable<ProjectTask> GetAllParentTasksOfProject(long projectId);
        IEnumerable<ProjectTask> FindParentTasksByProjectId(long projectid);
        IEnumerable<ProjectTask> FindProjectTaskByProjectId(long projectid, long taskId);
        TaskResponse GetAllTasks(TaskSearchRequest searchRequest);
        IEnumerable<ProjectTask> GetAllTasks(DateTime createdBefore);
        ProjectTask FindTaskWithPreRequisites(long id);
        IEnumerable<ProjectTask> GetProjectTasksByEmployeeId(long employeeId, long projectId);
        TaskResponse GetProjectTasksForEmployee(TaskSearchRequest searchRequest, long employeeId);
        TaskResponse GetProjectTasksForCustomer(TaskSearchRequest searchRequest, long customerId);
        IEnumerable<ProjectTask> GetAllNonSubTasks();
        IEnumerable<ProjectTask> GetTaskReportDetails(TaskReportCreateOrDetailsRequest request);
    }
}
