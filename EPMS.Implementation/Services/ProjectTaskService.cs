using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using FaceSharp.Api.Objects;

namespace EPMS.Implementation.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository Repository;
        private readonly ITaskEmployeeService TaskEmployeeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="taskEmployeeService"></param>
        public ProjectTaskService(IProjectTaskRepository repository, ITaskEmployeeService taskEmployeeService)
        {
            Repository = repository;
            TaskEmployeeService = taskEmployeeService;
        }

        public TaskResponse GetAllTasks(TaskSearchRequest searchRequest)
        {
            return Repository.GetAllTasks(searchRequest);
        }
        public ProjectTask FindProjectTaskById(long id)
        {
            return Repository.FindTaskWithPreRequisites(id);
        }

        public IEnumerable<ProjectTask> FindProjectTaskByProjectId(long projectid, long taskId)
        {
            return Repository.FindProjectTaskByProjectId(projectid, taskId);
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            return Repository.GetAll();
        }

        public IEnumerable<ProjectTask> GetTasksByProjectId(long projectId)
        {
            return Repository.GetTasksByProjectId(projectId);
        }

        public bool AddProjectTask(ProjectTask task, List<long> preReqList, List<long> assignedEmployee)
        {
            try
            {
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

                #endregion
                Repository.Update(tasks);
                Repository.SaveChanges();
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
            if (task != null)
            {
                if (task.TaskEmployees.Any())
                {
                    int count = task.TaskEmployees.Count();
                    for (int i = 0; i < count; i++)
                    {
                        TaskEmployeeService.DeleteTaskEmployee(task.TaskEmployees.FirstOrDefault());
                    }
                }
            }
            if (task != null)
            {
                if (task.PreRequisitTask.Any())
                {
                    int count = task.PreRequisitTask.Count();
                    for (int i = 0; i < count; i++)
                    {
                        //Repository.Delete(task.PreRequisitTask.FirstOrDefault());
                        task.PreRequisitTask.Remove(task.PreRequisitTask.FirstOrDefault());
                    }
                    Repository.SaveChanges();
                }
            }
            Repository.Delete(task);
            Repository.SaveChanges();
        }
    }
}
