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
            return project.ProjectId;
        }

        public long UpdateProject(Project project)
        {
            projectRepository.Update(project);
            projectRepository.SaveChanges();
            return project.ProjectId;
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
