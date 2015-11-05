using System;
using System.Globalization;
using System.Threading;

namespace EPMS.WebModels.ModelMappers.PMS
{
    public static class ProjectMapper
    {
        public static Models.DomainModels.Project CreateFromClientToServer(this WebsiteModels.Project source)
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
            return new Models.DomainModels.Project
            {
                ProjectId = source.ProjectId,
                NameE = source.NameE,
                NameA = source.NameA,
                CustomerId = source.CustomerId,
                OrderId = source.OrderId,
                SerialNo = source.SerialNo,
                DescriptionE = decspE,
                DescriptionA = decspA,
                StartDate = DateTime.ParseExact(source.StartDate.ToString(), "dd/MM/yyyy", new CultureInfo("en")),
                EndDate = DateTime.ParseExact(source.EndDate.ToString(), "dd/MM/yyyy", new CultureInfo("en")),
                Price = (decimal) source.Price,
                OtherCost = (decimal) source.OtherCost,
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
        public static WebsiteModels.Project CreateFromServerToClient(this Models.DomainModels.Project source)
        {
            WebsiteModels.Project project = new WebsiteModels.Project();
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
                decimal progress = 0;
                if (projectTask.TotalWeight > 0 && projectTask.ParentTask==null)
                {
                    progress = (decimal) projectTask.TaskProgress;
                }
                project.ProgressTotal += Convert.ToDouble(progress);
            }
            return project;
        }
        public static WebsiteModels.ProjectsForDDL CreateFromServerToClientForDdl(this Models.DomainModels.Project source)
        {
            WebsiteModels.ProjectsForDDL project = new WebsiteModels.ProjectsForDDL();
            project.ProjectId = source.ProjectId;
            project.NameE = source.NameE;
            project.NameA = source.NameA;
            project.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            project.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            foreach (var projectTask in source.ProjectTasks)
            {
                project.ProjectTasksSum += projectTask.TotalWeight !=0 ? Convert.ToInt32(projectTask.TotalWeight) : 0;
            }
            return project;
        }
    }
}