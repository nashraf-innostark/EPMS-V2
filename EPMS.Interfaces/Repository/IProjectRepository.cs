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
        IEnumerable<Project> GetProjectsForDashboard(string requester);
        IEnumerable<Project> FindProjectByCustomerId(long id);
    }
}
