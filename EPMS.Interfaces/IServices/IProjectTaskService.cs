using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProjectTaskService
    {
        ProjectTask FindProjectTaskById(long id);
        IEnumerable<ProjectTask> GetAll();
        long AddProjectTask(ProjectTask task);
        bool UpdateProjectTask(ProjectTask task);
        void DeleteProjectTask(ProjectTask task);
    }
}
