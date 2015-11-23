using System;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class ReportsMappers
    {

        public static ReportProject MapProjectToReportProject(this Project source)
        {
            var project = new ReportProject
            {
                ProjectId = source.ProjectId,
                NameE = source.NameE,
                NameA = source.NameA,
                StartDate = Convert.ToDateTime(source.StartDate),
                EndDate = Convert.ToDateTime(source.EndDate),
                Price = Convert.ToInt64(source.Price),
                OtherCost = Convert.ToInt64(source.OtherCost + source.ProjectTasks.Sum(x => x.TotalCost)),
                Status = source.Status,
                NoOfTasks = source.ProjectTasks.Count,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                CustomerNameE = source.Customer.CustomerNameE,
                CustomerNameA = source.Customer.CustomerNameA,

                NotesA = source.NotesA,
                NotesE = source.NotesE,
                DescriptionA = source.DescriptionA,
                DescriptionE = source.DescriptionE,
                NotesForCustomerA = source.NotesForCustomerA,
                NotesForCustomerE = source.NotesForCustomerE,
                SerialNo = source.SerialNo,
                RecCreatedBy = source.RecCreatedBy,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                ReportProjectTasks = source.ProjectTasks.Select(x=>x.MapProjectTaskToReportProjectTask()).ToList()
            };

            return project;
        }
        public static ReportProjectTask MapProjectTaskToReportProjectTask(this ProjectTask source)
        {
            ReportProjectTask projectTask = new ReportProjectTask
            {
                TaskId = source.TaskId,
                TaskNameE = source.TaskNameE,
                StartDate = Convert.ToDateTime(source.StartDate),
                EndDate = Convert.ToDateTime(source.EndDate),
                TotalCost = source.TotalCost,
                TotalWeight = source.TotalWeight,
                TaskProgress = source.TaskProgress,
                NotesA = source.NotesA,
                NotesE = source.NotesE,
                DeletedDate = source.DeletedDate,
                IsDeleted = source.IsDeleted,
                DescriptionA = source.DescriptionA,
                DescriptionE = source.DescriptionE,
                IsParent = source.IsParent,
                NoOfSubTasks = source.SubTasks.Count,
                ProjectId = source.ProjectId,
                TaskNameA = source.TaskNameA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                //ReportProjectSubTasks = source.SubTasks.Select(x=>x.MapProjectTaskToReportProjectTask()).ToList(),
                ReportTaskEmployees = source.TaskEmployees.Select(x=>x.MapTaskEmployeeToReportTaskEmployee()).ToList()
            };


            return projectTask;
        }
        public static ReportTaskEmployee MapTaskEmployeeToReportTaskEmployee(this TaskEmployee source)
        {
            ReportTaskEmployee projectTask = new ReportTaskEmployee
            {
                TaskId = source.TaskId,
                TaskEmployeeId = source.TaskEmployeeId,

                EmployeeFirstNameA = source.Employee.EmployeeFirstNameA,
                EmployeeFirstNameE = source.Employee.EmployeeFirstNameE,

                EmployeeMiddleNameA = source.Employee.EmployeeMiddleNameE,
                EmployeeMiddleNameE = source.Employee.EmployeeMiddleNameE,

                EmployeeLastNameA = source.Employee.EmployeeLastNameA,
                EmployeeLastNameE = source.Employee.EmployeeLastNameE,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };

            return projectTask;
        }

        public static ReportInventoryItem MapInventoryItemToReportInventoryItem(this InventoryItem source)
        {
            var reportInventoryItem = new ReportInventoryItem
            {
                InventoryItemId = source.ItemId,
                NameA = source.ItemNameAr,
                NameE = source.ItemNameEn,
                Price = (double) source.ItemVariations.Sum(x=>x.UnitPrice),
                Cost = (double) source.ItemVariations.Sum(x=>x.UnitCost),
            };

            return reportInventoryItem;
        }
    }
}
