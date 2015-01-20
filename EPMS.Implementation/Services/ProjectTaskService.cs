using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

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
            return Repository.Find(id);
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            return Repository.GetAll();
        }

        public IEnumerable<ProjectTask> GetTasksByProjectId(long projectId)
        {
            return Repository.GetTasksByProjectId(projectId);
        }

        public long AddProjectTask(ProjectTask task)
        {
            Repository.Add(task);
            Repository.SaveChanges();
            return task.TaskId;
        }

        public bool UpdateProjectTask(ProjectTask task)
        {
            try
            {
                Repository.Update(task);
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
