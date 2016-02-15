using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IProjectTaskRepository Repository;
        private readonly ITaskEmployeeService TaskEmployeeService;
        private readonly INotificationService notificationService;
        private readonly ICustomerService customerService;
        private readonly IEmployeeService employeeService;
        private readonly IProjectService projectService;
        private readonly IAspNetUserRepository aspNetUserRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationRepository"></param>
        /// <param name="repository"></param>
        /// <param name="taskEmployeeService"></param>
        /// <param name="notificationService"></param>
        public ProjectTaskService(INotificationRepository notificationRepository, IProjectTaskRepository repository, ITaskEmployeeService taskEmployeeService, INotificationService notificationService, ICustomerService customerService, IEmployeeService employeeService, IProjectService projectService, IAspNetUserRepository aspNetUserRepository)
        {
            this.notificationRepository = notificationRepository;
            Repository = repository;
            TaskEmployeeService = taskEmployeeService;
            this.notificationService = notificationService;
            this.customerService = customerService;
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.aspNetUserRepository = aspNetUserRepository;
        }

        public TaskResponse GetAllTasks(TaskSearchRequest searchRequest)
        {
            return Repository.GetAllTasks(searchRequest);
        }
        public ProjectTask FindProjectTaskById(long id)
        {
            return Repository.FindTaskWithPreRequisites(id);
        }
        public TaskResponse GetResponseForAddEdit(long id)
        {
            TaskResponse response = new TaskResponse();
            response.Customers = customerService.GetAll();
            response.Employees = employeeService.GetAll();
            response.Projects = projectService.GetAllProjects();

            if (id > 0)
            {
                response.ProjectTask = Repository.FindTaskWithPreRequisites(id);
                if (response.ProjectTask != null && response.ProjectTask.CustomerId != null)
                {
                    response.Projects = projectService.FindProjectByCustomerIdForEdit((long) response.ProjectTask.CustomerId);
                }
                if (response.ProjectTask != null)
                {
                    response.ProjectTasks = Repository.FindProjectTaskByProjectId(response.ProjectTask.ProjectId,
                        response.ProjectTask.TaskId);
                    response.AllParentTasks = Repository.GetAllParentTasksOfProject(response.ProjectTask.ProjectId).ToList();
                }
            }
            else
            {
                response.AllParentTasks = new List<ProjectTask>();
            }
            return response;
        }

        public CreateTaskReportsResponse GetAllProjectsAndTasks()
        {
            return new CreateTaskReportsResponse
            {
                Tasks = Repository.GetAllNonSubTasks().ToList(),
                Projects = projectService.GetAllProjects().ToList()
            };
        }

        public TaskResponse GetProjectTasksForEmployee(TaskSearchRequest searchRequest, long employeeId)
        {
            return Repository.GetProjectTasksForEmployee(searchRequest, employeeId);
        }
        public TaskResponse GetProjectTasksForCustomer(TaskSearchRequest searchRequest, long customerId)
        {
            return Repository.GetProjectTasksForCustomer(searchRequest, customerId);
        }

        public IEnumerable<ProjectTask> FindProjectTaskByProjectId(long projectid, long taskId)
        {
            return Repository.FindProjectTaskByProjectId(projectid, taskId);
        }

        public IEnumerable<ProjectTask> FindParentTasksByProjectId(long projectid)
        {
            return Repository.FindParentTasksByProjectId(projectid);
        }

        public IEnumerable<ProjectTaskResponse> LoadProjectTasksByEmployeeId(long employeeId, long projectId)
        {
            var tasks= Repository.GetProjectTasksByEmployeeId(employeeId, projectId);
            if (tasks.Any())
            {
                return tasks.Select(x => x.CreateForDashboard());
            }
            return null;
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            return Repository.GetAll().Where(x => !x.IsDeleted);
        }

        public IEnumerable<ProjectTask> GetTasksByProjectId(long projectId)
        {
            return Repository.GetTasksByProjectId(projectId);
        }

        public bool AddProjectTask(ProjectTask task, List<long> preReqList, List<long> assignedEmployee)
        {
            try
            {
                // update Parent task progress
                if (!task.IsParent && task.ParentTask != 0 && task.ParentTask != null)
                {
                    var parentTask = Repository.Find(Convert.ToInt32(task.ParentTask));
                    int countOtherTasksProgress = parentTask.SubTasks.Where(projectTask => projectTask.TaskId != task.TaskId).Sum(projectTask => Convert.ToInt32(projectTask.TaskProgress));
                    //int taskWeight = Convert.ToInt32(task.TotalWeight);
                    //var progressToAdd = Convert.ToInt32(task.TaskProgress) * taskWeight;
                    var progressToAdd = Convert.ToInt32(task.TaskProgress);
                    int parentTaskProgress = (countOtherTasksProgress + (progressToAdd));
                    if (parentTaskProgress <= 100)
                    {
                        parentTask.TaskProgress = parentTaskProgress;
                        //if (parentTaskProgress < 10)
                        //{
                        //    parentTask.TaskProgress = parentTaskProgress;
                        //}
                        Repository.Update(parentTask);
                        Repository.SaveChanges();
                    }
                }
                if (task.ParentTask == 0)
                {
                    task.ParentTask = null;
                }
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
                if (assignedEmployee.Any())
                {
                    task.TaskEmployees = new Collection<TaskEmployee>();
                    foreach (long employeeId in assignedEmployee)
                    {
                        TaskEmployee emp = new TaskEmployee { EmployeeId = employeeId, TaskId = task.TaskId };
                        task.TaskEmployees.Add(emp);
                    }

                    SendNotification(task, assignedEmployee);
                }
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateProjectTask(ProjectTask task, List<long> dbTaskList, List<long> clientTaskList, List<long> dbEmployeeList, List<long> clientEmployeeList)
        {
            try
            {
                // update Parent task progress
                if (!task.IsParent && task.ParentTask != 0 && task.ParentTask != null)
                {
                    var parentTask = Repository.Find(Convert.ToInt32(task.ParentTask));
                    //int countOtherTasksProgress = parentTask.SubTasks.Where(projectTask => projectTask.TaskId != task.TaskId).Sum(projectTask => Convert.ToInt32((Convert.ToDouble(projectTask.TaskProgress.Split('%')[0]) / Convert.ToDouble(projectTask.TotalWeight.Split('%')[0]))* 100));
                    double otherTasksProgress = 0;
                    foreach (var projectTask in parentTask.SubTasks)
                    {
                        if (projectTask.TaskId != task.TaskId && !projectTask.IsDeleted)
                        {
                            otherTasksProgress += Convert.ToDouble(projectTask.TaskProgress);
                        }
                    }
                    otherTasksProgress = otherTasksProgress + Convert.ToInt32(task.TaskProgress);
                    int parentTaskProgress = Convert.ToInt32(otherTasksProgress);
                    if (parentTaskProgress <= 100)
                    {
                        parentTask.TaskProgress = parentTaskProgress;
                        if (parentTaskProgress < 10)
                        {
                            parentTask.TaskProgress = parentTaskProgress;
                        }
                        Repository.Update(parentTask);
                        Repository.SaveChanges();
                    }
                }
                var tasks = Repository.Find(task.TaskId);
                var projectTaskToUpdate = tasks.CreateFromClientToServer(task);

                #region Pre-Req Task

                #region Add Pre-Req Tasks
                List<long> newPreReqTasksToAdd = new List<long>();
                // Compare new to db pre-tasks to Add new
                foreach (var requisitTask in clientTaskList)
                {
                    if (!dbTaskList.Contains(requisitTask))
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
                foreach (var requisitTask in dbTaskList)
                {
                    if (!clientTaskList.Contains(requisitTask))
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
                #endregion

                #region Task Employee

                #region Add Task Employees'
                List<long> newEmployeeToAdd = new List<long>();
                // Compare new to db pre-tasks to Add new
                foreach (var employee in clientEmployeeList)
                {
                    if (!dbEmployeeList.Contains(employee))
                    {
                        newEmployeeToAdd.Add(employee);
                    }
                }
                if (newEmployeeToAdd.Any())
                {
                    foreach (long employeeId in newEmployeeToAdd)
                    {
                        TaskEmployee emp = new TaskEmployee();
                        emp.EmployeeId = employeeId;
                        emp.TaskId = projectTaskToUpdate.TaskId;
                        if (emp != null)
                        {
                            tasks.TaskEmployees.Add(emp);
                        }
                    }
                }
                #endregion
                #region Remove Task Employees'
                List<long> newEmployeeToRemove = new List<long>();
                // Compare db pre-tasks to new to Remove
                foreach (var employee in dbEmployeeList)
                {
                    if (!clientEmployeeList.Contains(employee))
                    {
                        newEmployeeToRemove.Add(employee);
                    }
                }
                if (newEmployeeToRemove.Any())
                {
                    foreach (long employeeId in newEmployeeToRemove)
                    {
                        var taskemployee = tasks.TaskEmployees.FirstOrDefault(x => x.EmployeeId == employeeId);
                        if (taskemployee != null)
                        {
                            TaskEmployeeService.DeleteTaskEmployee(taskemployee);
                            //tasks.TaskEmployees.Remove(taskemployee);
                        }
                    }
                }
                #endregion

                // set IsDeleted to true for TaskEmployee if Task progress is equal to its weight
                if (task.TaskProgress == task.TotalWeight)
                {
                    var taskEmployees = TaskEmployeeService.GetTaskEmployeeByTaskId(task.TaskId).ToList();
                    foreach (var taskEmployee in taskEmployees)
                    {
                        taskEmployee.IsDeleted = true;
                        taskEmployee.DeletedDate = DateTime.Now;
                        TaskEmployeeService.UpdateTaskEmployeeWithOutNotification(taskEmployee);
                    }
                }

                #endregion

                Repository.Update(tasks);
                Repository.SaveChanges();

                SendNotification(task, clientEmployeeList);
                SendInstallmentNotification(tasks);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteProjectTask(long taskId)
        {
            ProjectTask task = Repository.Find(taskId);
            task.IsDeleted = true;
            task.DeletedDate = DateTime.Now;
            //if (task != null)
            //{
            //    if (task.TaskEmployees.Any())
            //    {
            //        int count = task.TaskEmployees.Count();
            //        for (int i = 0; i < count; i++)
            //        {
            //            TaskEmployeeService.DeleteTaskEmployee(task.TaskEmployees.FirstOrDefault());
            //        }
            //    }
            //}
            //if (task != null)
            //{
            //    if (task.PreRequisitTask.Any())
            //    {
            //        int count = task.PreRequisitTask.Count();
            //        for (int i = 0; i < count; i++)
            //        {
            //            //Repository.Delete(task.PreRequisitTask.FirstOrDefault());
            //            task.PreRequisitTask.Remove(task.PreRequisitTask.FirstOrDefault());
            //        }
            //        Repository.SaveChanges();
            //    }
            //}
            //Repository.Delete(task);
            Repository.Update(task);
            Repository.SaveChanges();
        }

        public void SendNotification(ProjectTask task, List<long> employeeIds)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();

            #region Send notification to admin
            if (Utility.IsDate(task.EndDate))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5,3, task.TaskId);

                notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["TaskDeliveryE"];
                notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["TaskDeliveryA"];
                notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["TaskDeliveryAlertBefore"]); //Days
                notificationViewModel.NotificationResponse.SystemGenerated = true;
                notificationViewModel.NotificationResponse.ForAdmin = true;
                notificationViewModel.NotificationResponse.ForRole = UserRole.Admin;

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 3; //Project task Delivery
                notificationViewModel.NotificationResponse.ItemId = task.TaskId; //Task
                notificationViewModel.NotificationResponse.AlertDate = task.EndDateOriginal;
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian

                notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
            }
            #endregion
            #region Send notification to assigned employees
            notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5,10, task.TaskId);

            notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["TaskAssignedE"];
            notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["TaskAssignedA"];
            notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["TaskAssignedAlertBefore"]); //Days
            notificationViewModel.NotificationResponse.SystemGenerated = true;
            notificationViewModel.NotificationResponse.ForAdmin = false;

            notificationViewModel.NotificationResponse.CategoryId = 5; //Project task
            notificationViewModel.NotificationResponse.SubCategoryId = 10; //Task delivery date
            notificationViewModel.NotificationResponse.ItemId = task.TaskId;
            if (DateTime.ParseExact(notificationViewModel.NotificationResponse.AlertDate, "dd/MM/yyyy", new CultureInfo("en")) > DateTime.Now)
            {
                notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
            }
            else
            {
                notificationViewModel.NotificationResponse.AlertDate = task.EndDateOriginal;
            }
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian

            notificationService.AddUpdateMeetingNotification(notificationViewModel, employeeIds);
            #endregion
        }

        public void SendInstallmentNotification(ProjectTask task)
        {
            Quotation quotation = new Quotation();
            decimal projectProgress = GetProjectProgress(task.Project);
            if (task.Project.OrderId != null)
            {
                quotation = task.Project.Order.Quotation;
            }
            if (task.Project.QuotationId != null)
            {
                quotation = task.Project.Quotation;
            }
            NotificationViewModel notificationViewModel = new NotificationViewModel
            {
                NotificationResponse = { SystemGenerated = true, ForAdmin = true, ForRole = UserRole.Admin }
            };
            // check which notification needs to send
            if (quotation.FirstInsDueAtCompletion > 0)
            {
                if (projectProgress >= quotation.FirstInsDueAtCompletion)
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 11, quotation.QuotationId);

                    if (notificationViewModel.NotificationResponse.NotificationId == 0)
                    {
                        notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["FirstInsDueE"];
                        notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["FirstInsDueA"];
                        notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["FirstInsDueAlertBefore"]); //Days

                        notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                        notificationViewModel.NotificationResponse.SubCategoryId = 11;//First Installment Due
                        notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                        notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
                        notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                        notificationViewModel.NotificationResponse.UserId =
                            aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                        notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                    }
                }
            }
            if (quotation.SecondInsDueAtCompletion > 0)
            {
                if (projectProgress >= quotation.SecondInsDueAtCompletion)
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 12, quotation.QuotationId);

                    if (notificationViewModel.NotificationResponse.NotificationId == 0)
                    {
                        notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["SecondInsDueE"];
                        notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["SecondInsDueA"];
                        notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["SecondInsDueAlertBefore"]); //Days

                        notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                        notificationViewModel.NotificationResponse.SubCategoryId = 12; // Second Installment Due
                        notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                        notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
                        notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                        notificationViewModel.NotificationResponse.UserId =
                            aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                        notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                    }
                }
            }
            if (quotation.ThirdInsDueAtCompletion > 0)
            {
                if (projectProgress >= quotation.ThirdInsDueAtCompletion)
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 13, quotation.QuotationId);

                    if (notificationViewModel.NotificationResponse.NotificationId == 0)
                    {
                        notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["ThirdInsDueE"];
                        notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["ThirdInsDueA"];
                        notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ThirdInsDueAlertBefore"]); //Days

                        notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                        notificationViewModel.NotificationResponse.SubCategoryId = 13; //third Installment Due
                        notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                        notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
                        notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                        notificationViewModel.NotificationResponse.UserId =
                            aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                        notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                    }
                }
            }
            if (quotation.FourthInsDueAtCompletion > 0)
            {
                if (projectProgress >= quotation.FourthInsDueAtCompletion)
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 14, quotation.QuotationId);

                    if (notificationViewModel.NotificationResponse.NotificationId == 0)
                    {
                        notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["FourthInsDueE"];
                        notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["FourthInsDueA"];
                        notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["FourthInsDueAlertBefore"]); //Days

                        notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                        notificationViewModel.NotificationResponse.SubCategoryId = 14; //fourth Installment Due
                        notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                        notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
                        notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                        notificationViewModel.NotificationResponse.UserId =
                            aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                        notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                    }
                }
            }
        }

        public decimal GetProjectProgress(Models.DomainModels.Project project)
        {
            return project.ProjectTasks.Where(x => x.ParentTask == null).Sum(x => x.TaskProgress != null ? (decimal)x.TaskProgress : 0);
        }
    }
}
