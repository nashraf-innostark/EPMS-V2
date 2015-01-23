using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;

namespace EPMS.Implementation.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository Repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public ProjectTaskService(IProjectTaskRepository repository)
        {
            Repository = repository;
        }

        public ProjectTask FindProjectTaskById(long id)
        {
            return Repository.FindTaskWithPreRequisites(id);
        }

        public IEnumerable<ProjectTask> FindProjectTaskByProjectId(long projectid)
        {
            return Repository.FindProjectTaskByProjectId(projectid);
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            return Repository.GetAll();
        }

        public IEnumerable<ProjectTask> GetTasksByProjectId(long projectId)
        {
            return Repository.GetTasksByProjectId(projectId);
        }

        public bool AddProjectTask(ProjectTask task, List<long> preReqList)
        {
            try
            {
                Repository.Add(task);
                if (preReqList.Any())
                {
                    task.PreRequisitTask = new Collection<ProjectTask>();
                    foreach (long taskId in preReqList)
                    {
                        var projTask = Repository.Find(taskId);
                        if (projTask != null)
                        {
                            task.PreRequisitTask.Add(projTask);
                        }
                    }
                }
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool UpdateProjectTask(ProjectTask task, List<long> dbList, List<long> clientList)
        {
            try
            {
                var tasks = Repository.Find(task.TaskId);
                var projectTaskToUpdate = task.CreateFromClientToServer(tasks);
                //Repository.Update(task);
                #region Add Pre-Req Tasks
                // create list of pre-req
                //if (dbList.Any())
                //{
                //    //task.PreRequisitTask = new Collection<ProjectTask>();
                //    foreach (long taskId in dbList)
                //    {
                //        var projTask = Repository.Find(taskId);
                //        if (projTask != null)
                //        {
                //            task.PreRequisitTask.Add(projTask);
                //        }
                //    }
                //}
                List<long> newPreReqTasksToAdd = new List<long>();
                // Compare new to db pre-tasks to Add new
                foreach (var requisitTask in clientList)
                {
                    if (!dbList.Contains(requisitTask))
                    {
                        newPreReqTasksToAdd.Add(requisitTask);
                    }
                }
                if (newPreReqTasksToAdd.Any())
                {
                    foreach (long taskId in newPreReqTasksToAdd)
                    {
                        var projTask = Repository.Find(taskId);
                        if (projTask != null)
                        {
                            tasks.PreRequisitTask.Add(projTask);
                        }
                    }
                }
                #endregion
                #region Remove Pre-Req Tasks
                List<long> newPreReqTasksToRemove = new List<long>();
                // Compare db pre-tasks to new to Remove
                foreach (var requisitTask in dbList)
                {
                    if (!clientList.Contains(requisitTask))
                    {
                        newPreReqTasksToRemove.Add(requisitTask);
                    }
                }
                if (newPreReqTasksToRemove.Any())
                {
                    foreach (long taskId in newPreReqTasksToRemove)
                    {
                        var projTask = Repository.Find(taskId);
                        if (projTask != null)
                        {
                            tasks.PreRequisitTask.Remove(projTask);
                        }
                    }
                }
                #endregion
                Repository.Update(tasks);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteProjectTask(ProjectTask task)
        {

        }
    }
}
