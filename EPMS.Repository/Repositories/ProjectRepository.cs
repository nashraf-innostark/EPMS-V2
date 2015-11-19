using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
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

        public IEnumerable<Project> GetAllOnGoingProjectsByEmployeeId(string id)
        {
            return DbSet.Where(x => x.Status != 4 && x.RecCreatedBy.Equals(id));//1 for Ongoing, 2 for On hold, 3 for Canceled, 4 for Finished
        }

        public IEnumerable<Project> GetAllFinishedProjectsByCustomerId(long id)
        {
            return DbSet.Where(x => x.Status == 4 && x.CustomerId == id);//1 for Ongoing, 2 for On hold, 3 for Canceled, 4 for Finished
        }

        public IEnumerable<Project> GetAllFinishedProjectsByEmployeeId(string id)
        {
            return DbSet.Where(x => x.Status == 4 && x.RecCreatedBy.Equals(id));//1 for Ongoing, 2 for On hold, 3 for Canceled, 4 for Finished
        }
        public IEnumerable<Project> GetProjectReportDetails(ProjectReportCreateOrDetailsRequest request)
        {
            var project=new Project();

            long customerid = 0;
            if (request.RequesterRole == "Customer")
                Int64.TryParse(request.RequesterId, out customerid);

            var response = request.RequesterRole != "Customer" ?
                DbSet.Include(x => x.ProjectTasks).Where(x => x.ProjectId.Equals(request.ProjectId)).ToList().Select(x=> new
                {
                    project=x,
                    ProjectTasks = x.ProjectTasks.Where(y=>y.RecCreatedDt<=request.ReportCreatedDate).ToList()
                }).Select(x=>x) :
                DbSet.Include(x => x.ProjectTasks).Where(x => x.ProjectId.Equals(request.ProjectId) && x.CustomerId.Equals(customerid)).ToList().Select(x => new
                {
                    project = x,
                    ProjectTasks = x.ProjectTasks.Where(y => y.RecCreatedDt <= request.ReportCreatedDate).ToList()
                }).Select(x => x);

            List<Project> projects=new List<Project>();
            foreach (var projectInResponse in response)
            {
                var singleProject = projectInResponse.project;
                singleProject.ProjectTasks = projectInResponse.ProjectTasks;
                projects.Add(singleProject);
            }
            return projects;
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

        public IEnumerable<Project> GetAllProjects(DateTime createdBefore)
        {
            return DbSet.Where(x => x.RecCreatedDate<=createdBefore).Include(x=>x.Customer);
        }
        public IEnumerable<Project> GetAllProjectsByEmployeeId(long employeeId)
        {
            return DbSet.Where(x => x.ProjectTasks.Any(y => y.TaskEmployees.Any(z => z.EmployeeId == employeeId)));
        }
    }
}
