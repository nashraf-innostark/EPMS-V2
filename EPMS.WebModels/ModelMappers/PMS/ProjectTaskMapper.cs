﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPMS.WebModels.WebsiteModels;
using ProjectTask = EPMS.Models.DomainModels.ProjectTask;
using EPMS.WebModels.WebsiteModels.Common;

namespace EPMS.WebModels.ModelMappers.PMS
{
    public static class ProjectTaskMapper
    {
        public static WebsiteModels.ProjectTask CreateForReport(this ProjectTask source)
        {
            WebsiteModels.ProjectTask projectTask = new WebsiteModels.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.TaskNameE = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.TaskNameE
                    : source.TaskNameA;
            projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = String.Format("{0:###.##}", source.TotalWeight);

            //projectTask.TaskProgressText = String.Format("{0:###.##}", source.TaskProgress) + "%";
            projectTask.TaskProgressText = source.TaskProgress < 100 ? ProjectStatus.OnGoing.ToString() : ProjectStatus.Finished.ToString();

            var progress = (source.TaskProgress != 0 || source.TotalWeight != 0) ? (source.TaskProgress / source.TotalWeight) * 100 : 0;
            projectTask.TaskProgress = progress != 0 ? String.Format("{0:###.##}", progress) : "0";
            
            
            return projectTask;
        }
        public static WebsiteModels.ProjectTask CreateFromServerToClientLv(this ProjectTask source)
        {
            WebsiteModels.ProjectTask projectTask = new WebsiteModels.ProjectTask();
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
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = String.Format("{0:###.##}", source.TotalWeight);
            projectTask.TaskProgressText = String.Format("{0:###.##}", source.TaskProgress) + "%";
            var progress = (source.TaskProgress != 0 || source.TotalWeight != 0) ? (source.TaskProgress / source.TotalWeight) * 100 : 0;
            projectTask.TaskProgress = progress != 0 ? String.Format("{0:###.##}", progress) : "0";
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
            projectTask.SubTasks = source.SubTasks.Any() ? source.SubTasks.Select(t => t.CreateFromServerToClientLv()).ToList() : new List<WebsiteModels.ProjectTask>();
            return projectTask;
        }
        public static WebsiteModels.ProjectTask CreateFromServerToClientCreate(this ProjectTask source)
        {
            WebsiteModels.ProjectTask projectTask = new WebsiteModels.ProjectTask();
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
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = String.Format("{0:###.##}", source.TotalWeight);
            projectTask.TaskProgressText = String.Format("{0:###.##}", source.TaskProgress) + "%";
            //var progress = (source.TaskProgress != 0 || source.TotalWeight != 0) ? (source.TaskProgress / source.TotalWeight) * 100 : 0;
            //projectTask.TaskProgress = progress != 0 ? String.Format("{0:###.##}", progress) : "0";
            projectTask.TaskProgress = source.TaskProgress != 0 ? String.Format("{0:###.##}", source.TaskProgress) : "0";
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
            projectTask.SubTasks = source.SubTasks.Any() ? source.SubTasks.Select(t => t.CreateFromServerToClientLv()).ToList() : new List<WebsiteModels.ProjectTask>();
            return projectTask;
        }
        public static WebsiteModels.ProjectTask CreateFromServerToClientCreateForProjectDetails(this ProjectTask source)
        {
            WebsiteModels.ProjectTask projectTask = new WebsiteModels.ProjectTask();
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
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = String.Format("{0:###.##}", source.TotalWeight);
            projectTask.TaskProgressText = String.Format("{0:###.##}", source.TaskProgress) + "%";
            var progress = (source.TaskProgress !=0 || source.TotalWeight != 0) ? (source.TaskProgress / source.TotalWeight) * 100 : 0;
            projectTask.TaskProgress = progress != 0 ? String.Format("{0:###.##}", progress) : "0";
            //projectTask.TaskProgress = String.Format("{0:###.##}", source.TaskProgress);
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
            projectTask.SubTasks = source.SubTasks.Any() ? source.SubTasks.Select(t => t.CreateFromServerToClientCreate()).ToList() : new List<WebsiteModels.ProjectTask>();
            projectTask.PrevTasksWeightSum = source.Project != null ? (source.Project.ProjectTasks.Sum(x => x.TotalWeight)-source.TotalWeight) ?? 0 : 0;
            return projectTask;
        }
        //{
        //    WebsiteModels.ProjectTask projectTask = new WebsiteModels.ProjectTask();
        //    projectTask.TaskId = source.TaskId;
        //    projectTask.ProjectId = source.ProjectId;
        //    projectTask.CustomerId = source.CustomerId;
        //    projectTask.TaskNameE = source.TaskNameE;
        //    projectTask.TaskNameA = source.TaskNameA;
        //    projectTask.IsParent = source.IsParent;
        //    projectTask.ParentTask = source.ParentTask;
        //    string descpEn = "";
        //    if (!String.IsNullOrEmpty(source.DescriptionE))
        //    {
        //        descpEn = source.DescriptionE.Replace("\n", "");
        //        descpEn = descpEn.Replace("\r", "");
        //    }
        //    projectTask.DescriptionE = descpEn;
        //    string descpAr = "";
        //    if (!String.IsNullOrEmpty(source.DescriptionA))
        //    {
        //        descpAr = source.DescriptionA.Replace("\n", "");
        //        descpAr = descpAr.Replace("\r", "");
        //    }
        //    projectTask.DescriptionA = descpAr;
        //    projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
        //    projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
        //    projectTask.TotalCost = source.TotalCost;
        //    projectTask.TotalWeight = String.Format("{0:###.##}", source.TotalWeight);
        //    projectTask.TaskProgress = String.Format("{0:###.##}", source.TaskProgress);
        //    //projectTask.TaskProgress = String.Format("{0:###.##}", source.TaskProgress);
        //    string notesEn = "";
        //    if (!String.IsNullOrEmpty(source.NotesE))
        //    {
        //        notesEn = source.NotesE.Replace("\n", "");
        //        notesEn = notesEn.Replace("\r", "");
        //    }
        //    projectTask.NotesE = notesEn;
        //    string notesAr = "";
        //    if (!String.IsNullOrEmpty(source.NotesA))
        //    {
        //        notesAr = source.NotesA.Replace("\n", "");
        //        notesAr = notesAr.Replace("\r", "");
        //    }
        //    projectTask.NotesA = notesAr;
        //    projectTask.RecCreatedBy = source.RecCreatedBy;
        //    projectTask.RecCreatedDt = source.RecCreatedDt;
        //    projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
        //    projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
        //    if (source.Project != null)
        //    {
        //        projectTask.ProjectNameE = source.Project.NameE;
        //        projectTask.ProjectNameA = source.Project.NameA;
        //    }
        //    projectTask.RequisitTasks = source.PreRequisitTask.Select(x => x.CreateFromServerToClientChild()).ToList();
        //    if (source.TaskEmployees != null)
        //    {
        //        projectTask.TaskEmployees = source.TaskEmployees.Select(x => x.CreateFromServerToClient()).ToList();
        //    }
        //    if (source.PreRequisitTask.Count > 0)
        //    {
        //        foreach (var preRequisitTask in source.PreRequisitTask)
        //        {
        //            projectTask.PreReqTasks = preRequisitTask.TaskNameE + " - " + projectTask.PreReqTasks;
        //        }
        //        projectTask.PreReqTasks = projectTask.PreReqTasks.Substring(0, projectTask.PreReqTasks.Length - 3);
        //    }
        //    if (source.TaskEmployees != null && source.TaskEmployees.Count > 0)
        //    {
        //        foreach (var employee in source.TaskEmployees)
        //        {
        //            var empFullName = employee.Employee.EmployeeFirstNameE + " " + employee.Employee.EmployeeMiddleNameE +
        //                              " " + employee.Employee.EmployeeLastNameE;
        //            projectTask.EmployeesAssigned = empFullName + " - " + projectTask.EmployeesAssigned;
        //        }
        //        projectTask.EmployeesAssigned = projectTask.EmployeesAssigned.Substring(0, projectTask.EmployeesAssigned.Length - 3);
        //    }
        //    projectTask.SubTasks = source.SubTasks.Any() ? source.SubTasks.Select(t => t.CreateFromServerToClientCreate()).ToList() : new List<WebsiteModels.ProjectTask>();
        //    projectTask.PrevTasksWeightSum = source.Project != null ? (source.Project.ProjectTasks.Sum(x => x.TotalWeight) - source.TotalWeight) ?? 0 : 0;
        //    return projectTask;
        //}

        public static ProjectTask CreateFromClientToServer(this WebsiteModels.ProjectTask source)
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
            projectTask.TotalWeight = Convert.ToDecimal(source.TotalWeight);
            projectTask.TaskProgress = Convert.ToDecimal(source.TaskProgress);
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

        public static WebsiteModels.ProjectTask CreateFromServerToClientChild(this ProjectTask source)
        {
            WebsiteModels.ProjectTask projectTask = new WebsiteModels.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.TotalWeight = String.Format("{0:###.##}", source.TotalWeight);
            projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            return projectTask;
        }
        public static WebsiteModels.ProjectTask CreateFromServerToClientForEmployee(this ProjectTask source)
        {
            WebsiteModels.ProjectTask projectTask = new WebsiteModels.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.ProjectNameE = source.Project.NameE;
            projectTask.ProjectNameA = source.Project.NameA;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            return projectTask;
        }
        public static WebsiteModels.ProjectTask CreateFromServerToClientParentTasks(this ProjectTask source)
        {
            WebsiteModels.ProjectTask projectTask = new WebsiteModels.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.ProjectNameE = source.Project.NameE;
            projectTask.ProjectNameA = source.Project.NameA;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.TotalWeight = String.Format("{0:###.##}", source.TotalWeight);
            projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            foreach (var subTask in source.SubTasks)
            {
                WebsiteModels.SubTaskWeight model = new WebsiteModels.SubTaskWeight
                {
                    TaskId = subTask.TaskId,
                    TaskWeight = String.Format("{0:###.##}", subTask.TotalWeight) != "" ? Convert.ToInt32(String.Format("{0:###.##}", subTask.TotalWeight)) : 0
                };
                projectTask.SubTasksWeight.Add(model);
            }
            return projectTask;
        }

        public static ProjectTaskDropDown CreateFoDropDown(this ProjectTask source)
        {
            return new ProjectTaskDropDown
            {
                TaskId = source.TaskId,
                TaskNameE = source.TaskNameE,
                TaskNameA = source.TaskNameA,
                ProjectId = source.ProjectId
            };
        }
    }
}