using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProjectTaskService
    {
        ProjectTask FindProjectTaskById(long id);
        ProjectTask FindProjectTaskByProjectId(long projectid);
        IEnumerable<ProjectTask> GetAll();
        IEnumerable<ProjectTask> GetTasksByProjectId(long projectId);
        long AddProjectTask(ProjectTask task);
        bool UpdateProjectTask(ProjectTask task);
        void DeleteProjectTask(ProjectTask task);
    }
}
