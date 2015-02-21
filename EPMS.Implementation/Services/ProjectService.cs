using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.ModelMapers;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using Project = EPMS.Models.DomainModels.Project;

namespace EPMS.Implementation.Services
{
    public class ProjectService:IProjectService
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly INotificationService notificationService;
        private readonly IProjectRepository projectRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IProjectTaskRepository projectTaskRepository;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectService(INotificationRepository notificationRepository,IAspNetUserRepository aspNetUserRepository,INotificationService notificationService,IProjectRepository projectRepository,IOrdersRepository ordersRepository,IProjectTaskRepository projectTaskRepository)
        {
            this.notificationRepository = notificationRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            this.notificationService = notificationService;
            this.projectRepository = projectRepository;
            this.ordersRepository = ordersRepository;
            this.projectTaskRepository = projectTaskRepository;
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

        public IEnumerable<Project> GetAllProjects()
        {
            return projectRepository.GetAll();
        }

        public long AddProject(Project project)
        {
            projectRepository.Add(project);
            projectRepository.SaveChanges();
            SetOrderStatus(project);
            SendNotification(project);
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
            SendNotification(project);
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

        public ProjectResponseForDashboard LoadProjectForDashboard(string requester, long projectId)
        {
            var project = projectRepository.GetProjectForDashboard(requester, projectId);
            if (project != null)
            {
                var projectTasks = projectTaskRepository.GetTasksByProjectId(project.ProjectId);
                ProjectResponseForDashboard projectResponse = new ProjectResponseForDashboard();
                projectResponse.Project = project.CreateForDashboard();
                if (projectTasks.Any())
                    projectResponse.ProjectTasks = projectTasks.Select(x=>x.CreateForDashboard());
                return projectResponse;
            }
            return null;
        }

        public IEnumerable<Project> LoadAllProjects(string requester, int status)
        {
            return projectRepository.GetAllProjects(requester, status);
        }

        public IEnumerable<Project> LoadAllProjectsByEmployeeId(long employeeId)
        {
            return projectRepository.GetAllProjectsByEmployeeId(employeeId);
        }

        public void SendNotification(Project project)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();

            #region Project End date for admins
            if (Utility.IsDate(project.EndDate))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(6, project.ProjectId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 6; //Project
                notificationViewModel.NotificationResponse.SubCategoryId = project.ProjectId; //Ended
                notificationViewModel.NotificationResponse.AlertBefore = 2; //1 Week
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(project.EndDate).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = true;

                notificationService.AddUpdateNotification(notificationViewModel);
            }
            #endregion

            #region Project End date
            if (Utility.IsDate(project.EndDate))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(9, project.ProjectId);

                notificationViewModel.NotificationResponse.TitleE = "Project has been finished.";
                notificationViewModel.NotificationResponse.TitleA = "Project has been finished.";

                notificationViewModel.NotificationResponse.CategoryId = 9; //Project
                notificationViewModel.NotificationResponse.SubCategoryId = project.ProjectId; //Ended
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(project.EndDate).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = false;//not for admins
                notificationViewModel.NotificationResponse.UserId = aspNetUserRepository.GetUserIdByCustomerId(Convert.ToInt64(project.CustomerId));

                notificationService.AddUpdateNotification(notificationViewModel);
            }
            #endregion
            #region Project Start date
            if (Utility.IsDate(project.StartDate))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(10, project.ProjectId);

                notificationViewModel.NotificationResponse.TitleE = "Project has been started.";
                notificationViewModel.NotificationResponse.TitleA = "Project has been started.";

                notificationViewModel.NotificationResponse.CategoryId = 10; //Project
                notificationViewModel.NotificationResponse.SubCategoryId = project.ProjectId; //Started
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(project.StartDate).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                
                notificationService.AddUpdateNotification(notificationViewModel);
            }
            #endregion
        }
    }
}
