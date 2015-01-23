using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class ProjectTaskMapper
    {
        public static ProjectTask CreateFromClientToServer(this ProjectTask source, ProjectTask projectTask)
        {
            //ProjectTask projectTask = new ProjectTask();
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
            projectTask.PreRequisitTask = source.PreRequisitTask.Select(x => x.CreateFromClientToServer(null)).ToList();
            return projectTask;
        }
        public static PreRequisitTask CreateFromServerToClient(this PreRequisitTask source)
        {
            PreRequisitTask caseType = new PreRequisitTask
            {
                TaskId = source.TaskId,
                PreReqTaskId = source.PreReqTaskId,
            };
            return caseType;
        }
    }
}
