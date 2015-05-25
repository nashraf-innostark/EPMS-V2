using System;
using System.Globalization;
using System.Threading;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class ProjectMapper
    {
        public static Project CreateFromClientToServer(this Models.Project source)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.DescriptionE))
            {
                decspE = source.DescriptionE.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.DescriptionA))
            {
                decspA = source.DescriptionA.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
            }
            string notesE = "";
            string notesA = "";
            if (!String.IsNullOrEmpty(source.NotesE))
            {
                notesE = source.NotesE.Replace("\n", "");
                notesE = notesE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.NotesA))
            {
                notesA = source.NotesA.Replace("\n", "");
                notesA = notesA.Replace("\r", "");
            }
            string notesForCusE = "";
            string notesForCusA = "";
            if (!String.IsNullOrEmpty(source.NotesForCustomerE))
            {
                notesForCusE = source.NotesForCustomerE.Replace("\n", "");
                notesForCusE = notesForCusE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.NotesForCustomerA))
            {
                notesForCusA = source.NotesForCustomerA.Replace("\n", "");
                notesForCusA = notesForCusA.Replace("\r", "");
            }
            return new Project
            {
                ProjectId = source.ProjectId,
                NameE = source.NameE,
                NameA = source.NameA,
                CustomerId = source.CustomerId,
                OrderId = source.OrderId,
                SerialNo = source.SerialNo,
                DescriptionE = decspE,
                DescriptionA = decspA,
                StartDate = DateTime.ParseExact(source.StartDate, "dd/MM/yyyy", new CultureInfo("en")),
                EndDate = DateTime.ParseExact(source.EndDate, "dd/MM/yyyy", new CultureInfo("en")),
                Price = source.Price,
                OtherCost = source.OtherCost,
                Status = source.Status,
                NotesE = notesE,
                NotesA = notesA,
                NotesForCustomerE = notesForCusE,
                NotesForCustomerA = notesForCusA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                QuotationId = source.QuotationId
            };
        }
        public static Models.Project CreateFromServerToClient(this Project source)
        {
            Models.Project project=new Models.Project();
            project.ProjectId = source.ProjectId;
            project.NameE = source.NameE;
            project.NameA = source.NameA;
            project.CustomerId = source.CustomerId;
            project.OrderId = source.OrderId;
            project.QuotationId = source.QuotationId;
            project.SerialNo = source.SerialNo;
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.DescriptionE))
            {
                decspE = source.DescriptionE.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.DescriptionA))
            {
                decspA = source.DescriptionA.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
            }
            string notesE = "";
            string notesA = "";
            if (!String.IsNullOrEmpty(source.NotesE))
            {
                notesE = source.NotesE.Replace("\n", "");
                notesE = notesE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.NotesA))
            {
                notesA = source.NotesA.Replace("\n", "");
                notesA = notesA.Replace("\r", "");
            }
            string notesForCusE = "";
            string notesForCusA = "";
            if (!String.IsNullOrEmpty(source.NotesForCustomerE))
            {
                notesForCusE = source.NotesForCustomerE.Replace("\n", "");
                notesForCusE = notesForCusE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.NotesForCustomerA))
            {
                notesForCusA = source.NotesForCustomerA.Replace("\n", "");
                notesForCusA = notesForCusA.Replace("\r", "");
            }
            project.DescriptionE = decspE;
            project.DescriptionA = decspA;
            project.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            project.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            project.Price = Convert.ToInt64(source.Price);
            project.OtherCost = Convert.ToInt64(source.OtherCost);
            project.Status = source.Status;
            project.NotesE = notesE;
            project.NotesA = notesA;
            project.NotesForCustomerE = notesForCusE;
            project.NotesForCustomerA = notesForCusA;
            project.RecCreatedBy = source.RecCreatedBy;
            project.RecCreatedDate = source.RecCreatedDate;
            project.RecLastUpdatedBy = source.RecLastUpdatedBy;
            project.RecLastUpdatedDate = source.RecLastUpdatedDate;
            if (source.CustomerId > 0)
            {
                project.CustomerNameE = source.Customer.CustomerNameE;
                project.CustomerNameA = source.Customer.CustomerNameA;
            }
            foreach (var projectTask in source.ProjectTasks)
            {
                double taskWeight = 0;
                if (projectTask.TotalWeight != "")
                {
                    var taskTotalWeight = projectTask.TotalWeight.Split('%');
                    taskWeight = Convert.ToInt32(taskTotalWeight[0]);
                    var tempTaskWeight = projectTask.TaskProgress.Split('%');
                    taskWeight = (Convert.ToInt32(tempTaskWeight[0]) * taskWeight);
                }
                project.ProgressTotal += Convert.ToDouble(taskWeight/100);
            }
            return project;
        }
        public static Models.ProjectsForDDL CreateFromServerToClientForDdl(this Project source)
        {
            Models.ProjectsForDDL project = new Models.ProjectsForDDL();
            project.ProjectId = source.ProjectId;
            project.NameE = source.NameE;
            project.NameA = source.NameA;
            project.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            project.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            foreach (var projectTask in source.ProjectTasks)
            {
                project.ProjectTasksSum += Convert.ToInt32(projectTask.TotalWeight.Split('%')[0]);
            }
            return project;
        }
    }
}