﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;

namespace EPMS.Repository.Repositories
{
    public class EmployeeJobHistoryRepository: BaseRepository<EmployeeJobHistory>, IEmployeeJobHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeJobHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<EmployeeJobHistory> DbSet
        {
            get { return db.EmployeeJobHistory; }
        }

        public IEnumerable<EmployeeJobHistory> GetJobHistoryByEmployeeId(long empId)
        {
            return DbSet.Where(x => x.EmployeeId == empId);
        }

        #endregion
    }
}
