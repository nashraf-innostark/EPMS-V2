﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class DIFHistoryRepository : BaseRepository<DIFHistory>, IDIFHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DIFHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<DIFHistory> DbSet
        {
            get { return db.DifHistories; }
        }

        #endregion
        public IEnumerable<DIFHistory> GetDifHistoryData()
        {
            return DbSet.Where(x => x.Status == 3 || x.Status == 2).ToList();
        }
    }
}