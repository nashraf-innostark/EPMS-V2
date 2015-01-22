using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ProjectDocumentService:IProjectDocumentService
    {
        private readonly IProjectDocumentRepository documentRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectDocumentService(IProjectDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        #endregion
        public IEnumerable<ProjectDocument> FindProjectDocumentsByProjectId(long projectId)
        {
            return documentRepository.LoadProjectDocumentsByProjectId(projectId);
        }

        public bool AddProjectDocument(ProjectDocument document)
        {
            documentRepository.Add(document);
            documentRepository.SaveChanges();
            return true;
        }
        
        public bool Delete(long documentId)
        {
            documentRepository.Delete(documentRepository.Find(documentId));
            documentRepository.SaveChanges();
            return true;
        }

        public ProjectDocument FindProjectDocumentsById(long documentId)
        {
            return documentRepository.Find(documentId);
        }
    }
}
