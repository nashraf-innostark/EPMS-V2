using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class ProjectMapper
    {
        public static ResponseModels.Project CreateForDashboard(this Project source)
        {
            ResponseModels.Project project = new ResponseModels.Project();
            project.ProjectId = source.ProjectId;
            project.NameA = source.NameA;
            project.NameAShort = source.NameA.Length > 5 ? source.NameA.Substring(0, 5) + "..." : source.NameA;
            project.NameE = source.NameE;
            project.NameEShort = source.NameE.Length > 5 ? source.NameE.Substring(0, 5) + "..." : source.NameE;
            project.ProgressTotal = source.ProjectTasks != null && source.ProjectTasks.Any()
                ? source.ProjectTasks.Sum(projectTask => Convert.ToDouble(projectTask.TaskProgress))
                : 0;
            //project.ProgressTotal = Math.Ceiling(project.ProgressTotal);
            //foreach (var projectTask in source.ProjectTasks)
            //{
            //    decimal progress = 0;
            //    if (projectTask.TotalWeight > 0 && projectTask.ParentTask == null)
            //    {
            //        progress = (decimal)projectTask.TaskProgress;
            //    }
            //    project.ProgressTotal += Convert.ToDouble(progress);
            //}
            return project;
        }

        public static Project UpdateDbDataFromClient(this Project destination, Project source)
        {
            destination.ProjectId = source.ProjectId;
            destination.NameE = source.NameE;
            destination.NameA = source.NameA;
            destination.CustomerId = source.CustomerId;
            destination.OrderId = source.OrderId;
            destination.SerialNo = source.SerialNo;
            destination.DescriptionE = source.DescriptionE;
            destination.DescriptionA = source.DescriptionA;
            destination.StartDate = source.StartDate;
            destination.EndDate = source.EndDate;
            destination.Price = source.Price;
            destination.OtherCost = source.OtherCost;
            destination.Status = source.Status;
            destination.NotesE = source.NotesE;
            destination.NotesA = source.NotesA;
            destination.NotesForCustomerE = source.NotesForCustomerE;
            destination.NotesForCustomerA = source.NotesForCustomerA;
            destination.RecLastUpdatedBy = source.RecLastUpdatedBy;
            destination.RecLastUpdatedDate = source.RecLastUpdatedDate;
            destination.QuotationId = source.QuotationId;
            return destination;
        }
    }
}
