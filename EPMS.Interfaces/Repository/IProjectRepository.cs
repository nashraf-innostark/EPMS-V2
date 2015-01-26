using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProjectRepository : IBaseRepository<Project, long>
    {
        IEnumerable<Project> GetAllOnGoingProjects();
        IEnumerable<Project> GetAllFinishedProjects();
        IEnumerable<Project> GetAllOnGoingProjectsByCustomerId(long id);
        IEnumerable<Project> GetAllFinishedProjectsByCustomerId(long id);
        Project GetProjectForDashboard(string requester, long projectId, int status);
        IEnumerable<Project> FindProjectByCustomerId(long id);
    }
}
