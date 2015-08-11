using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers.PMS
{
    public static class PreRequisitTasksMapper
    {
        public static WebsiteModels.PreRequisitTask CreateFromServerToClient(this PreRequisitTask source)
        {
            WebsiteModels.PreRequisitTask caseType = new WebsiteModels.PreRequisitTask
            {
                TaskId = source.TaskId,
                PreReqTask = source.PreReqTaskId,
            };
            return caseType;
        }
    }
}