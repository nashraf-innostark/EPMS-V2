﻿using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Repository.BaseRepository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class TaskEmployeeRepository : BaseRepository<TaskEmployee>, ITaskEmployeeRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskEmployeeRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TaskEmployee> DbSet
        {
            get { return db.TaskEmployees; }
        }

        #endregion

        public int CountTasksByEmployeeId(long id)
        {
            return DbSet.Count(x => x.EmployeeId == id);
        }
    }
}
