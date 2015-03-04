using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ProjectDocumentRepository : BaseRepository<ProjectDocument>, IProjectDocumentRepository
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectDocumentRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProjectDocument> DbSet
        {
            get { return db.ProjectDocuments; }
        }

        #endregion

        public IEnumerable<ProjectDocument> LoadProjectDocumentsByProjectId(long projectId)
        {
            return DbSet.Where(x => x.ProjectId == projectId);
        }
    }
}
