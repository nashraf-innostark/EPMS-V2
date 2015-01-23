using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProjectTaskService
    {
        ProjectTask FindProjectTaskById(long id);
        IEnumerable<ProjectTask> FindProjectTaskByProjectId(long projectid);
        IEnumerable<ProjectTask> GetAll();
        IEnumerable<ProjectTask> GetTasksByProjectId(long projectId);
        bool AddProjectTask(ProjectTask task, List<long> preReqList, List<long> assignedEmployee);
        bool UpdateProjectTask(ProjectTask task, List<long> dbTaskList, List<long> clientTaskList, List<long> dbEmployeeList, List<long> clientEmployeeList);
        void DeleteProjectTask(ProjectTask task);
    }
}
