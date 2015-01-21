using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class PreRequisitTasksMapper
    {
        public static Models.PreRequisitTask CreateFromServerToClient(this PreRequisitTask source)
        {
            Models.PreRequisitTask caseType = new Models.PreRequisitTask
            {
                PreReqTaskId = source.PreReqTaskId,
                TaskId = source.TaskId,
                PreReqTask = source.PreReqTask,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
            return caseType;
        }
    }
}