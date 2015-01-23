using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class TaskEmployeeService : ITaskEmployeeService
    {
        private readonly ITaskEmployeeRepository TaskEmployeeRepository;

        public TaskEmployeeService(ITaskEmployeeRepository taskEmployeeRepository)
        {
            TaskEmployeeRepository = taskEmployeeRepository;
        }

        public TaskEmployee FindAllowanceById(long id)
        {
            return TaskEmployeeRepository.Find(id);
        }

        public IEnumerable<TaskEmployee> GetAll()
        {
            return TaskEmployeeRepository.GetAll();
        }

        public bool AddTaskEmployee(TaskEmployee employee)
        {
            try
            {
                TaskEmployeeRepository.Add(employee);
                TaskEmployeeRepository.SaveChanges();
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
                TaskEmployeeRepository.Update(employee);
                TaskEmployeeRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteTaskEmployee(TaskEmployee employee)
        {
            TaskEmployeeRepository.Delete(employee);
            TaskEmployeeRepository.SaveChanges();
        }
    }
}
