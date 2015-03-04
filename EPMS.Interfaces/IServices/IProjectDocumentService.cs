using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IProjectDocumentService
    {
        IEnumerable<ProjectDocument> FindProjectDocumentsByProjectId(long projectId);
        bool AddProjectDocument(ProjectDocument complaint);
        bool Delete(long documentId);
        ProjectDocument FindProjectDocumentsById(long documentId);
    }
}
