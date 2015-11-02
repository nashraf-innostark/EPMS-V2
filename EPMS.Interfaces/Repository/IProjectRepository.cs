using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProjectRepository : IBaseRepository<Project, long>
    {
        IEnumerable<Project> GetAllOnGoingProjects();
        IEnumerable<Project> GetAllFinishedProjects();
        IEnumerable<Project> GetAllOnGoingProjectsByCustomerId(long id);
        IEnumerable<Project> GetAllOnGoingProjectsByEmployeeId(string id);
        IEnumerable<Project> GetAllFinishedProjectsByCustomerId(long id);
        IEnumerable<Project> GetAllFinishedProjectsByEmployeeId(string id);
        Project GetProjectForDashboard(string requester, long projectId);
        IEnumerable<Project> FindProjectByCustomerId(long id);
        IEnumerable<Project> GetAllProjects(string requester, int status);
        IEnumerable<Project> GetAllProjectsByEmployeeId(long employeeId);
    }
}
