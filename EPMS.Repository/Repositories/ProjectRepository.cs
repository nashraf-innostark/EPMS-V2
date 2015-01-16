using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Project> DbSet
        {
            get { return db.Projects; }
        }

        #endregion

        public IEnumerable<Project> GetAllProjectsByCustomerId(long id)
        {
            return DbSet.Where(x => x.CustomerId == id);
        }
    }
}
