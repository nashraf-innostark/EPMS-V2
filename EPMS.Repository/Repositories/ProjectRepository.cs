using System;
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

        public Project GetProjectForDashboard(string requester, long projectId)
        {
            if (requester == "Admin")
            {
                if (projectId == 0)
                {
                    return DbSet.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
                }
                return DbSet.Where(x=>x.ProjectId==projectId).OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
            }
            long customerId = Convert.ToInt64(requester);
            if (projectId == 0)
            {
                return DbSet.Where(x=> x.CustomerId == customerId).OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
            }
            return DbSet.Where(x => x.ProjectId == projectId && x.CustomerId == customerId).OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
        }

        public IEnumerable<Project> FindProjectByCustomerId(long id)
        {
            return DbSet.Where(project => project.CustomerId == id && project.Status != 4);
        }

        public IEnumerable<Project> GetAllProjects(string requester, int status)
        {
            if (requester == "Admin")
            {
                return DbSet.Where(x => x.Status == status);
            }
           long customerId = Convert.ToInt64(requester);
                return DbSet.Where(x => x.CustomerId==customerId && x.Status == status);
        }

        public IEnumerable<Project> GetAllProjectsByEmployeeId(long employeeId)
        {
            return DbSet.Where(x => x.ProjectTasks.Any(y => y.TaskEmployees.Any(z => z.EmployeeId == employeeId)));
        }
    }
}
