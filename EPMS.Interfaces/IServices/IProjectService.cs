using System.Collections.Generic;
using EPMS.Models.ResponseModels;
using Project = EPMS.Models.DomainModels.Project;

namespace EPMS.Interfaces.IServices
{
    public interface IProjectService
    {
        Project FindProjectById(long id);
        IEnumerable<Project> FindProjectByCustomerId(long id);
        long AddProject(Project complaint);
        long UpdateProject(Project complaint);
        IEnumerable<Project> LoadAllUnfinishedProjects();
        IEnumerable<Project> LoadAllFinishedProjects();
        IEnumerable<Project> LoadAllUnfinishedProjectsByCustomerId(long id);
        IEnumerable<Project> LoadAllFinishedProjectsByCustomerId(long id);
        ProjectResponseForDashboard LoadProjectForDashboard(string requester, long projectId, int status);
    }
}
