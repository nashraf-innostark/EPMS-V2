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
    }
}
