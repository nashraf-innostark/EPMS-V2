using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProjectRepository : IBaseRepository<Project, long>
    {
        IEnumerable<Project> GetAllProjectsByCustomerId(long id);
    }
}
