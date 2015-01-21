using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectTaskRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProjectTask> DbSet
        {
            get { return db.ProjectTasks; }
        }
        #endregion

        public IEnumerable<ProjectTask> GetTasksByProjectId(long projectId)
        {
            return DbSet.Where(x => x.ProjectId == projectId);
        }

        public ProjectTask FindProjectTaskByProjectId(long projectid)
        {
            return DbSet.FirstOrDefault(x => x.ProjectId == projectid);
        }
    }
}
