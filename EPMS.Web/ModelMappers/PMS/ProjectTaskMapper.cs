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
            projectTask.TaskProgress = source.TaskProgress;
            projectTask.RecCreatedBy = source.RecCreatedBy;
            projectTask.RecCreatedDt = source.RecCreatedDt;
            projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
            projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
            projectTask.RequisitTasks = source.PreRequisitTasks.Select(x => x.CreateFromServerToClient());
            if (source.PreRequisitTasks.Count > 0)
            {
                foreach (var preRequisitTask in source.PreRequisitTasks)
                {
                    projectTask.PreReqTasks = preRequisitTask.ProjectTask.TaskNameE + " - " + projectTask.PreReqTasks;
                }
                projectTask.PreReqTasks = projectTask.PreReqTasks.Substring(0, projectTask.PreReqTasks.Length - 3);
            }
            
            return projectTask;
        }
    }
}