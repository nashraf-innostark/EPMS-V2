using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
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
        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<TaskByColumn, Func<ProjectTask, object>> taskClause =

            new Dictionary<TaskByColumn, Func<ProjectTask, object>>
                    {
                        { TaskByColumn.TaskName,  c => c.TaskNameE},
                        { TaskByColumn.ProjectName,  c => c.Project.NameE},
                        { TaskByColumn.StartDate,  c => c.StartDate},
                        { TaskByColumn.DeliveryDate,  c => c.EndDate},
                        { TaskByColumn.Cost,  c => c.TotalCost},
                        { TaskByColumn.Progress,  c => c.TaskProgress},
                    };
        #endregion

        public IEnumerable<ProjectTask> GetTasksByProjectId(long projectId)
        {
            return DbSet.Where(x => x.ProjectId == projectId);
        }

        public IEnumerable<ProjectTask> GetAllParentTasks()
        {
            return DbSet.Where(pt => pt.IsParent);
        }

        public IEnumerable<ProjectTask> FindParentTasksByProjectId(long projectid)
        {
            return DbSet.Where(t => t.ProjectId == projectid && t.IsParent);
        }

        public IEnumerable<ProjectTask> FindProjectTaskByProjectId(long projectid, long taskId)
        {
            return DbSet.Where(x => x.ProjectId == projectid && x.TaskId != taskId);
        }
        public ProjectTask FindTaskWithPreRequisites(long id)
        {
            return DbSet.Include(x => x.PreRequisitTask).SingleOrDefault(x => x.TaskId == id);
        }

        public IEnumerable<ProjectTask> GetProjectTasksByEmployeeId(long employeeId, long projectId)
        {
            if(projectId>0)
                return DbSet.Where(x => x.ProjectId==projectId && x.TaskEmployees.Any(y => y.EmployeeId == employeeId));
            return DbSet.Where(x => x.TaskEmployees.Any(y => y.EmployeeId == employeeId));
        }

        public TaskResponse GetAllTasks(TaskSearchRequest searchRequest)
        {
            if (searchRequest.iSortCol_0 == 1)
            {
                searchRequest.iSortCol_0 = 2;
            }
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;
            //DateTime startDate = Convert.ToDateTime(searchRequest.SearchString);
            //DateTime endDate = Convert.ToDateTime(searchRequest.SearchString);
            //decimal totalCost = Convert.ToDecimal(searchRequest.SearchString);
            //int taskProgress = Convert.ToInt32(searchRequest.SearchString);

            Expression<Func<ProjectTask, bool>> query =
                s => ((string.IsNullOrEmpty(searchRequest.SearchString)) || ((s.TaskNameE.Contains(searchRequest.SearchString)) ||
                    (s.TaskNameA.Contains(searchRequest.SearchString)) || (s.Project.NameE.Contains(searchRequest.SearchString)) ||
                    (s.Project.NameA.Contains(searchRequest.SearchString))));
                    //|| (s.StartDate == startDate) ||
                    //(s.EndDate == endDate) || (s.TotalCost.Equals(totalCost)) || (s.TaskProgress.Equals(taskProgress))));

            IEnumerable<ProjectTask> tasks = searchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(taskClause[searchRequest.TaskByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(taskClause[searchRequest.TaskByColumn]).Skip(fromRow).Take(toRow).ToList();
            //var countOfTasks = DbSet.Count(x => x.ParentTask == null);
            return new TaskResponse { ProjectTasks = tasks, TotalDisplayRecords = DbSet.Where(x => x.ParentTask == null).Count(query), TotalRecords = DbSet.Where(x => x.ParentTask == null).Count(query) };
        }

        public TaskResponse GetProjectTasksForEmployee(TaskSearchRequest searchRequest, long employeeId)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;

            Expression<Func<ProjectTask, bool>> query =
                s => (s.TaskEmployees.Any(y => y.EmployeeId == employeeId) && ((string.IsNullOrEmpty(searchRequest.SearchString)) || ((s.TaskNameE.Contains(searchRequest.SearchString)) ||
                    (s.TaskNameA.Contains(searchRequest.SearchString)) || (s.Project.NameE.Contains(searchRequest.SearchString)) ||
                    (s.Project.NameA.Contains(searchRequest.SearchString)))));
            //|| (s.StartDate == startDate) ||
            //(s.EndDate == endDate) || (s.TotalCost.Equals(totalCost)) || (s.TaskProgress.Equals(taskProgress))));

            IEnumerable<ProjectTask> tasks = searchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(taskClause[searchRequest.TaskByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(taskClause[searchRequest.TaskByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new TaskResponse { ProjectTasks = tasks, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count(query) };
        }
        public TaskResponse GetProjectTasksForCustomer(TaskSearchRequest searchRequest, long customerId)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;

            Expression<Func<ProjectTask, bool>> query =
                s => (s.CustomerId == customerId && ((string.IsNullOrEmpty(searchRequest.SearchString)) || ((s.TaskNameE.Contains(searchRequest.SearchString)) ||
                    (s.TaskNameA.Contains(searchRequest.SearchString)) || (s.Project.NameE.Contains(searchRequest.SearchString)) ||
                    (s.Project.NameA.Contains(searchRequest.SearchString)))));
            //|| (s.StartDate == startDate) ||
            //(s.EndDate == endDate) || (s.TotalCost.Equals(totalCost)) || (s.TaskProgress.Equals(taskProgress))));

            IEnumerable<ProjectTask> tasks = searchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(taskClause[searchRequest.TaskByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(taskClause[searchRequest.TaskByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new TaskResponse { ProjectTasks = tasks, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count(query) };
        }
    }
}
