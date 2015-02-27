using System;
using System.Globalization;
using System.Linq;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Repository.Repositories;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class ProjectMapper
    {
        private static IProjectTaskService projectTaskService;
        public static Project CreateFromClientToServer(this Models.Project source)
        {
            return new Project
            {
                ProjectId = source.ProjectId,
                NameE = source.NameE,
                NameA = source.NameA,
                CustomerId = source.CustomerId,
                OrderId = source.OrderId,
                SerialNo = source.SerialNo,
                DescriptionE = source.DescriptionE,
                DescriptionA = source.DescriptionA,
                StartDate = DateTime.ParseExact(source.StartDate, "dd/MM/yyyy", new CultureInfo("en")),
                EndDate = DateTime.ParseExact(source.EndDate, "dd/MM/yyyy", new CultureInfo("en")),
                Price = source.Price,
                OtherCost = source.OtherCost,
                Status = source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                NotesForCustomerE = source.NotesForCustomerE,
                NotesForCustomerA = source.NotesForCustomerA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
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
            project.SerialNo = source.SerialNo;
            project.DescriptionE = source.DescriptionE;
            project.DescriptionA = source.DescriptionA;
            project.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            project.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            project.Price = Convert.ToInt64(source.Price);
            project.OtherCost = Convert.ToInt64(source.OtherCost);
            project.Status = source.Status;
            project.NotesE = source.NotesE;
            project.NotesA = source.NotesA;
            project.NotesForCustomerE = source.NotesForCustomerE;
            project.NotesForCustomerA = source.NotesForCustomerA;
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
                    taskWeight = Convert.ToInt32(projectTask.TaskProgress * taskWeight);
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
            return project;
        }
    }
}