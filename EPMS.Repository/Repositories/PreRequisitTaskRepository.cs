using System.Data.Entity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class PreRequisitTaskRepository : BaseRepository<PreRequisitTask> , IPreRequisitTaskService
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PreRequisitTaskRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PreRequisitTask> DbSet
        {
            get { return db.PreRequisitTasks; }
        }

        #endregion
    }
}
