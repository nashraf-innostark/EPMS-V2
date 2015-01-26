using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.ResponseModels;
using Project = EPMS.Models.DomainModels.Project;

namespace EPMS.Implementation.Services
{
    public class ProjectService:IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IOrdersRepository ordersRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectService(IProjectRepository projectRepository,IOrdersRepository ordersRepository)
        {
            this.projectRepository = projectRepository;
            this.ordersRepository = ordersRepository;
        }

        #endregion

        public Project FindProjectById(long id)
        {
           return projectRepository.Find(id);
        }

        public IEnumerable<Project> FindProjectByCustomerId(long id)
        {
            return projectRepository.FindProjectByCustomerId(id);
        }

        public long AddProject(Project project)
        {
            projectRepository.Add(project);
            projectRepository.SaveChanges();
            SetOrderStatus(project);
            return project.ProjectId;
        }

        private void SetOrderStatus(Project project)
        {
            var order = ordersRepository.Find(Convert.ToInt32(project.OrderId));
            if (order == null) return;
            order.OrderStatus = project.Status;
            ordersRepository.Update(order);
            ordersRepository.SaveChanges();
        }

        public long UpdateProject(Project project)
        {
            projectRepository.Update(project);
            projectRepository.SaveChanges();
            SetOrderStatus(project);
            return project.ProjectId;
        }

        public IEnumerable<Project> LoadAllUnfinishedProjects()
        {
            return projectRepository.GetAllOnGoingProjects();
        }

        public IEnumerable<Project> LoadAllFinishedProjects()
        {
            return projectRepository.GetAllFinishedProjects();
        }

        public IEnumerable<Project> LoadAllUnfinishedProjectsByCustomerId(long id)
        {
            return projectRepository.GetAllOnGoingProjectsByCustomerId(id);
        }

        public IEnumerable<Project> LoadAllFinishedProjectsByCustomerId(long id)
        {
            return projectRepository.GetAllFinishedProjectsByCustomerId(id);
        }

        public ProjectResponseForDashboard LoadProjectForDashboard(string requester, long projectId, int status)
        {
            var project = projectRepository.GetProjectForDashboard(requester, projectId, status);
            return null;
        }
    }
}
