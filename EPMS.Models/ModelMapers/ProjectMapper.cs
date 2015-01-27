using System;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class ProjectMapper
    {
        public static ResponseModels.Project CreateForDashboard(this Project source)
        {
            return new ResponseModels.Project
            {
                ProjectId = source.ProjectId,
                NameA = source.NameA,
                NameAShort = source.NameA.Length > 15 ? source.NameA.Substring(0, 15) + "..." : source.NameA,
                NameE = source.NameE,
                NameEShort = source.NameE.Length > 15 ? source.NameE.Substring(0, 15) + "..." : source.NameE,
                ProgressTotal = source.ProjectTasks.Any()?source.ProjectTasks.Sum(projectTask => Convert.ToInt32(projectTask.TaskProgress)):0
            };
        }
    }
}
