﻿using System.Collections.Generic;
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

        public IEnumerable<Project> GetAllOnGoingProjects()
        {
            return DbSet.Where(x => x.Status != 4);//1 for Ongoing, 2 for On hold, 3 for Canceled, 4 for Finished
        }

        public IEnumerable<Project> GetAllFinishedProjects()
        {
            return DbSet.Where(x => x.Status == 4);//1 for Ongoing, 2 for On hold, 3 for Canceled, 4 for Finished
        }

        public IEnumerable<Project> GetAllOnGoingProjectsByCustomerId(long id)
        {
            return DbSet.Where(x => x.Status != 4 && x.CustomerId == id);//1 for Ongoing, 2 for On hold, 3 for Canceled, 4 for Finished
        }

        public IEnumerable<Project> GetAllFinishedProjectsByCustomerId(long id)
        {
            return DbSet.Where(x => x.Status == 4 && x.CustomerId == id);//1 for Ongoing, 2 for On hold, 3 for Canceled, 4 for Finished
        }

        public IEnumerable<Project> GetProjectsForDashboard(string requester)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Project> FindProjectByCustomerId(long id)
        {
            return DbSet.Where(project => project.CustomerId == id);
        }
    }
}
