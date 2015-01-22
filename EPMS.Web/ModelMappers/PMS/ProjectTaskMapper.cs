using System.Collections.Generic;
using System.Linq;
using System;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class ProjectTaskMapper
    {
        public static Models.ProjectTask CreateFromServerToClient(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.DescriptionE = source.DescriptionE;
            projectTask.DescriptionA = source.DescriptionA;
            projectTask.StartDate = source.StartDate;
            projectTask.EndDate = source.EndDate;
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = source.TotalWeight;
            projectTask.TaskProgress = source.TaskProgress;
            projectTask.NotesE = source.NotesE;
            projectTask.NotesA = source.NotesA;
            projectTask.RecCreatedBy = source.RecCreatedBy;
            projectTask.RecCreatedDt = source.RecCreatedDt;
            projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
            projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
            projectTask.RequisitTasks = source.PreRequisitTask.Select(x => x.CreateFromServerToClient()).ToList();
            projectTask.TaskEmployees = source.TaskEmployees.Select(x => x.CreateFromServerToClient()).ToList();
            if (source.PreRequisitTask.Count > 0)
            {
                foreach (var preRequisitTask in source.PreRequisitTask)
                {
                    projectTask.PreReqTasks = preRequisitTask.TaskNameE + " - " + projectTask.PreReqTasks;
                }
                projectTask.PreReqTasks = projectTask.PreReqTasks.Substring(0, projectTask.PreReqTasks.Length - 3);
            }
            if (source.TaskEmployees.Count > 0)
            {
                foreach (var employee in source.TaskEmployees)
                {
                    projectTask.EmployeesAssigned = employee.Employee.EmployeeNameE + " - " + projectTask.EmployeesAssigned;
                }
                projectTask.EmployeesAssigned = projectTask.EmployeesAssigned.Substring(0, projectTask.EmployeesAssigned.Length - 3);
            }
            return projectTask;
        }

        public static ProjectTask CreateFromClientToServer(this Models.ProjectTask source)
        {
            ProjectTask projectTask = new ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.DescriptionE = source.DescriptionE;
            projectTask.DescriptionA = source.DescriptionA;
            projectTask.StartDate = source.StartDate;
            projectTask.EndDate = source.EndDate;
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = source.TotalWeight;
            projectTask.TaskProgress = source.TaskProgress;
            projectTask.NotesE = source.NotesE;
            projectTask.NotesA = source.NotesA;
            projectTask.RecCreatedBy = source.RecCreatedBy;
            projectTask.RecCreatedDt = source.RecCreatedDt;
            projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
            projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
            return projectTask;
        }
    }
}