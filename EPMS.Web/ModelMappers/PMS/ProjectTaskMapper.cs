using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPMS.Web.Models;
using ProjectTask = EPMS.Models.DomainModels.ProjectTask;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class ProjectTaskMapper
    {
        public static Models.ProjectTask CreateFromServerToClientLv(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.IsParent = source.IsParent;
            projectTask.ParentTask = source.ParentTask;
            string descpEn = "";
            if (!String.IsNullOrEmpty(source.DescriptionE))
            {
                descpEn = source.DescriptionE.Replace("\n", "");
                descpEn = descpEn.Replace("\r", "");
            }
            projectTask.DescriptionE = descpEn;
            string descpAr = "";
            if (!String.IsNullOrEmpty(source.DescriptionA))
            {
                descpAr = source.DescriptionA.Replace("\n", "");
                descpAr = descpAr.Replace("\r", "");
            }
            projectTask.DescriptionA = descpAr;
            projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.TotalCost = source.TotalCost ?? 0;
            projectTask.TotalWeight = source.TotalWeight;
            var progress = (Convert.ToDouble(source.TaskProgress.Split('%')[0]) / Convert.ToDouble(source.TotalWeight.Split('%')[0])) * 100;
            projectTask.TaskProgress = progress + "%";
            string notesEn = "";
            if (!String.IsNullOrEmpty(source.NotesE))
            {
                notesEn = source.NotesE.Replace("\n", "");
                notesEn = notesEn.Replace("\r", "");
            }
            projectTask.NotesE = notesEn;
            string notesAr = "";
            if (!String.IsNullOrEmpty(source.NotesA))
            {
                notesAr = source.NotesA.Replace("\n", "");
                notesAr = notesAr.Replace("\r", "");
            }
            projectTask.NotesA = notesAr;
            projectTask.RecCreatedBy = source.RecCreatedBy;
            projectTask.RecCreatedDt = source.RecCreatedDt;
            projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
            projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
            if (source.Project != null)
            {
                projectTask.ProjectNameE = source.Project.NameE;
                projectTask.ProjectNameA = source.Project.NameA;
            }
            projectTask.RequisitTasks = source.PreRequisitTask.Select(x => x.CreateFromServerToClientChild()).ToList();
            if (source.TaskEmployees != null)
            {
                projectTask.TaskEmployees = source.TaskEmployees.Select(x => x.CreateFromServerToClient()).ToList();
            }
            if (source.PreRequisitTask.Count > 0)
            {
                foreach (var preRequisitTask in source.PreRequisitTask)
                {
                    projectTask.PreReqTasks = preRequisitTask.TaskNameE + " - " + projectTask.PreReqTasks;
                }
                projectTask.PreReqTasks = projectTask.PreReqTasks.Substring(0, projectTask.PreReqTasks.Length - 3);
            }
            if (source.TaskEmployees != null && source.TaskEmployees.Count > 0)
            {
                foreach (var employee in source.TaskEmployees)
                {
                    var empFullName = employee.Employee.EmployeeFirstNameE + " " + employee.Employee.EmployeeMiddleNameE +
                                      " " + employee.Employee.EmployeeLastNameE;
                    projectTask.EmployeesAssigned = empFullName + " - " + projectTask.EmployeesAssigned;
                }
                projectTask.EmployeesAssigned = projectTask.EmployeesAssigned.Substring(0, projectTask.EmployeesAssigned.Length - 3);
            }
            projectTask.SubTasks = source.SubTasks.Any() ? source.SubTasks.Select(t => t.CreateFromServerToClientLv()).ToList() : new List<Models.ProjectTask>();
            return projectTask;
        }
        public static Models.ProjectTask CreateFromServerToClientCreate(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.IsParent = source.IsParent;
            projectTask.ParentTask = source.ParentTask;
            string descpEn = "";
            if (!String.IsNullOrEmpty(source.DescriptionE))
            {
                descpEn = source.DescriptionE.Replace("\n", "");
                descpEn = descpEn.Replace("\r", "");
            }
            projectTask.DescriptionE = descpEn;
            string descpAr = "";
            if (!String.IsNullOrEmpty(source.DescriptionA))
            {
                descpAr = source.DescriptionA.Replace("\n", "");
                descpAr = descpAr.Replace("\r", "");
            }
            projectTask.DescriptionA = descpAr;
            projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.TotalCost = source.TotalCost ?? 0;
            projectTask.TotalWeight = source.TotalWeight;
            projectTask.TaskProgress = source.TaskProgress;
            string notesEn = "";
            if (!String.IsNullOrEmpty(source.NotesE))
            {
                notesEn = source.NotesE.Replace("\n", "");
                notesEn = notesEn.Replace("\r", "");
            }
            projectTask.NotesE = notesEn;
            string notesAr = "";
            if (!String.IsNullOrEmpty(source.NotesA))
            {
                notesAr = source.NotesA.Replace("\n", "");
                notesAr = notesAr.Replace("\r", "");
            }
            projectTask.NotesA = notesAr;
            projectTask.RecCreatedBy = source.RecCreatedBy;
            projectTask.RecCreatedDt = source.RecCreatedDt;
            projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
            projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
            if (source.Project != null)
            {
                projectTask.ProjectNameE = source.Project.NameE;
                projectTask.ProjectNameA = source.Project.NameA;
            }
            projectTask.RequisitTasks = source.PreRequisitTask.Select(x => x.CreateFromServerToClientChild()).ToList();
            if (source.TaskEmployees != null)
            {
                projectTask.TaskEmployees = source.TaskEmployees.Select(x => x.CreateFromServerToClient()).ToList();
            }
            if (source.PreRequisitTask.Count > 0)
            {
                foreach (var preRequisitTask in source.PreRequisitTask)
                {
                    projectTask.PreReqTasks = preRequisitTask.TaskNameE + " - " + projectTask.PreReqTasks;
                }
                projectTask.PreReqTasks = projectTask.PreReqTasks.Substring(0, projectTask.PreReqTasks.Length - 3);
            }
            if (source.TaskEmployees != null && source.TaskEmployees.Count > 0)
            {
                foreach (var employee in source.TaskEmployees)
                {
                    var empFullName = employee.Employee.EmployeeFirstNameE + " " + employee.Employee.EmployeeMiddleNameE +
                                      " " + employee.Employee.EmployeeLastNameE;
                    projectTask.EmployeesAssigned = empFullName + " - " + projectTask.EmployeesAssigned;
                }
                projectTask.EmployeesAssigned = projectTask.EmployeesAssigned.Substring(0, projectTask.EmployeesAssigned.Length - 3);
            }
            projectTask.SubTasks = source.SubTasks.Any() ? source.SubTasks.Select(t => t.CreateFromServerToClientCreate()).ToList() : new List<Models.ProjectTask>();
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
            projectTask.IsParent = source.IsParent;
            projectTask.ParentTask = source.ParentTask;
            string descpEn = "";
            if (!String.IsNullOrEmpty(source.DescriptionE))
            {
                descpEn = source.DescriptionE.Replace("\n", "");
                descpEn = descpEn.Replace("\r", "");
            }
            projectTask.DescriptionE = descpEn;
            string descpAr = "";
            if (!String.IsNullOrEmpty(source.DescriptionA))
            {
                descpAr = source.DescriptionA.Replace("\n", "");
                descpAr = descpAr.Replace("\r", "");
            }
            projectTask.DescriptionA = descpAr;
            projectTask.EndDateOriginal = source.EndDate;
            projectTask.StartDate = source.StartDate == null ? (DateTime?)null : DateTime.ParseExact(source.StartDate, "dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = source.EndDate == null ? (DateTime?)null : DateTime.ParseExact(source.EndDate, "dd/MM/yyyy", new CultureInfo("en"));
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = source.TotalWeight;
            source.TaskProgress = projectTask.TaskProgress;
            //projectTask.TaskProgress = source.TaskProgress;
            string notesEn = "";
            if (!String.IsNullOrEmpty(source.NotesE))
            {
                notesEn = source.NotesE.Replace("\n", "");
                notesEn = notesEn.Replace("\r", "");
            }
            projectTask.NotesE = notesEn;
            string notesAr = "";
            if (!String.IsNullOrEmpty(source.NotesA))
            {
                notesAr = source.NotesA.Replace("\n", "");
                notesAr = notesAr.Replace("\r", "");
            }
            projectTask.NotesA = notesAr;
            projectTask.RecCreatedBy = source.RecCreatedBy;
            projectTask.RecCreatedDt = source.RecCreatedDt;
            projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
            projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
            projectTask.PreRequisitTask = source.RequisitTasks.Select(x=>x.CreateFromClientToServer()).ToList();
            return projectTask;
        }

        public static Models.ProjectTask CreateFromServerToClientChild(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.TotalWeight = source.TotalWeight;
            projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            return projectTask;
        }
        public static Models.ProjectTask CreateFromServerToClientForEmployee(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.ProjectNameE = source.Project.NameE;
            projectTask.ProjectNameA = source.Project.NameA;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            return projectTask;
        }
        public static Models.ProjectTask CreateFromServerToClientParentTasks(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.ProjectNameE = source.Project.NameE;
            projectTask.ProjectNameA = source.Project.NameA;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.TotalWeight = source.TotalWeight;
            projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            foreach (var subTask in source.SubTasks)
            {
                SubTaskWeight model = new SubTaskWeight
                {
                    TaskId = subTask.TaskId,
                    TaskWeight = Convert.ToInt32(subTask.TotalWeight.Split('%')[0])
                };
                projectTask.SubTasksWeight.Add(model);
            }
            return projectTask;
        }
    }
}