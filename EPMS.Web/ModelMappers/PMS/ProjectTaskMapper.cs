using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class ProjectTaskMapper
    {
        public static Models.ProjectTask CreateFromServerToClient(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
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