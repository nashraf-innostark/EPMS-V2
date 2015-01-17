using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ProjectService:IProjectService
    {
        private readonly IProjectRepository projectRepository;
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        #endregion

        public Project FindProjectById(long id)
        {
            throw new System.NotImplementedException();
        }

        public bool AddProject(Project complaint)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateProject(Project complaint)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Project> LoadAllOnGoingProjects()
        {
            return projectRepository.GetAllOnGoingProjects();
        }

        public IEnumerable<Project> LoadAllFinishedProjects()
        {
            return projectRepository.GetAllFinishedProjects();
        }

        public IEnumerable<Project> LoadAllOnGoingProjectsByCustomerId(long id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Project> LoadAllFinishedProjectsByCustomerId(long id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Project> LoadProjectsForDashboard(string requester)
        {
            throw new System.NotImplementedException();
        }
    }
}
