using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProjectService
    {
        Project FindProjectById(long id);
        bool AddProject(Project complaint);
        bool UpdateProject(Project complaint);
        IEnumerable<Project> LoadAllProjects();
        IEnumerable<Project> LoadAllProjectsByCustomerId(long id);
        IEnumerable<Project> LoadProjectsForDashboard(string requester);
    }
}
