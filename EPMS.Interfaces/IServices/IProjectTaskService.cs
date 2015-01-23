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
        bool AddProjectTask(ProjectTask task, List<long> preReqList);
        bool UpdateProjectTask(ProjectTask task, List<long> preReqList);
        void DeleteProjectTask(ProjectTask task);
    }
}
