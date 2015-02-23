using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class TaskEmployeeService : ITaskEmployeeService
    {
        private readonly ITaskEmployeeRepository taskEmployeeRepository;
        private readonly INotificationService notificationService;

        public TaskEmployeeService(ITaskEmployeeRepository taskEmployeeRepository,INotificationService notificationService)
        {
            this.taskEmployeeRepository = taskEmployeeRepository;
            this.notificationService = notificationService;
        }

        public TaskEmployee FindTaskEmployeeById(long id)
        {
            return taskEmployeeRepository.Find(id);
        }
        public int CountTasksByEmployeeId(long id)
        {
            return taskEmployeeRepository.CountTasksByEmployeeId(id);
        }

        public IEnumerable<TaskEmployee> GetAll()
        {
            return taskEmployeeRepository.GetAll();
        }
        public IEnumerable<TaskEmployee> GetTaskEmployeeByEmployeeId(long employeeId)
        {
            return taskEmployeeRepository.GetTaskEmployeeByEmployeeId(employeeId);
        }
        public bool AddTaskEmployee(TaskEmployee employee)
        {
            try
            {
                taskEmployeeRepository.Add(employee);
                taskEmployeeRepository.SaveChanges();
                //SendNotification(employee);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTaskEmployee(TaskEmployee employee)
        {
            try
            {
                taskEmployeeRepository.Update(employee);
                taskEmployeeRepository.SaveChanges();
                //SendNotification(employee);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteTaskEmployee(TaskEmployee employee)
        {
            taskEmployeeRepository.Delete(employee);
            taskEmployeeRepository.SaveChanges();
        }

        public void SendNotification(TaskEmployee employee)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();

            #region Send notification to admin

            notificationViewModel.NotificationResponse.TitleE = "You have been assigned a task.";
            notificationViewModel.NotificationResponse.TitleA = "You have been assigned a task.";

            notificationViewModel.NotificationResponse.CategoryId = 5; //Other
            notificationViewModel.NotificationResponse.AlertBefore = 3; //1 Day
            notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.AddDays(-1).ToShortDateString();
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
            //notificationViewModel.NotificationResponse.EmployeeId = employee.EmployeeId;

            notificationService.AddUpdateNotification(notificationViewModel);

            #endregion
        }
    }
}
