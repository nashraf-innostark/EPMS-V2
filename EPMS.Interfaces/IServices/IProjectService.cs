using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProjectService
    {
        Project FindProjectById(long id);
        IEnumerable<Project> FindProjectByCustomerId(long id);
        long AddProject(Project complaint);
        long UpdateProject(Project complaint);
        IEnumerable<Project> LoadAllOnGoingProjects();
        IEnumerable<Project> LoadAllFinishedProjects();
        IEnumerable<Project> LoadAllOnGoingProjectsByCustomerId(long id);
        IEnumerable<Project> LoadAllFinishedProjectsByCustomerId(long id);
        IEnumerable<Project> LoadProjectsForDashboard(string requester);
    }
}
