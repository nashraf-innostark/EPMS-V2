using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IProjectDocumentRepository : IBaseRepository<ProjectDocument, long>
    {
        IEnumerable<ProjectDocument> LoadProjectDocumentsByProjectId(long projectId);
    }
}
