﻿using System.Data.Entity;
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
    }
}
