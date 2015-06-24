using System;
using System.Collections.Generic;
using System.Configuration;
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

            #region Project Delivery date for admins
            if (Utility.IsDate(project.EndDate))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5,9, project.ProjectId);

                notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["ProjectDeliveryE"];
                notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["ProjectDeliveryA"];
                notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ProjectDeliveryAlertBefore"]); //Days
                notificationViewModel.NotificationResponse.SystemGenerated = true;
                notificationViewModel.NotificationResponse.ForAdmin = true;

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 9; // Project Delivery
                notificationViewModel.NotificationResponse.ItemId = project.ProjectId; //Ended
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(project.EndDate).ToString("dd/MM/yyyy");
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian

                notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
            }
            #endregion
            #region If project is of any customer, then notify the customer

            if (project.CustomerId > 0)
            {
                #region Project End date
                if (Utility.IsDate(project.EndDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                            notificationRepository.GetNotificationsIdByCategories(5, 2, project.ProjectId);

                    notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["ProjectFinishedE"];
                    notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["ProjectFinishedA"];
                    notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ProjectFinishedAlertBefore"]); //Days
                    notificationViewModel.NotificationResponse.SystemGenerated = true;
                    notificationViewModel.NotificationResponse.ForAdmin = false;

                    notificationViewModel.NotificationResponse.CategoryId = 5; //Project
                    notificationViewModel.NotificationResponse.SubCategoryId = 2; //Ended
                    notificationViewModel.NotificationResponse.ItemId = project.ProjectId;

                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(project.EndDate).ToString("dd/MM/yyyy");
                    notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                    notificationViewModel.NotificationResponse.UserId = aspNetUserRepository.GetUserIdByCustomerId(Convert.ToInt64(project.CustomerId));

                    notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                }
                #endregion

                #region Project Start date
                if (Utility.IsDate(project.StartDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                            notificationRepository.GetNotificationsIdByCategories(5, 8, project.ProjectId);

                    notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["ProjectStartedE"];
                    notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["ProjectStartedA"];
                    notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ProjectStartedAlertBefore"]); //Days
                    notificationViewModel.NotificationResponse.SystemGenerated = true;
                    notificationViewModel.NotificationResponse.ForAdmin = false;

                    notificationViewModel.NotificationResponse.CategoryId = 5; //Project
                    notificationViewModel.NotificationResponse.SubCategoryId = 8; //Started
                    notificationViewModel.NotificationResponse.ItemId = project.ProjectId; //Ended

                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(project.StartDate).ToString("dd/MM/yyyy");
                    notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                    notificationViewModel.NotificationResponse.UserId = aspNetUserRepository.GetUserIdByCustomerId(Convert.ToInt64(project.CustomerId));

                    notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                }
                #endregion
            }
            #endregion
        }
    }
}
