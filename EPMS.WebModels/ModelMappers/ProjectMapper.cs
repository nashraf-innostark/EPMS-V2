using System;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class ProjectMapper
    {
        public static Models.ResponseModels.Project CreateForDashboard(this Project source)
        {
            Models.ResponseModels.Project project=new Models.ResponseModels.Project();
            project.ProjectId = source.ProjectId;
            project.NameA = source.NameA;
            project.NameAShort = source.NameA.Length > 5 ? source.NameA.Substring(0, 5) + "..." : source.NameA;
            project.NameE = source.NameE;
            project.NameEShort = source.NameE.Length > 5 ? source.NameE.Substring(0, 5) + "..." : source.NameE;
            project.ProgressTotal = source.ProjectTasks.Any()
                ? source.ProjectTasks.Sum(projectTask => (Convert.ToDouble(projectTask.TaskProgress.Split('%').FirstOrDefault())*Convert.ToDouble(projectTask.TotalWeight.Split('%').FirstOrDefault()))/100)
                : 0;
            project.ProgressTotal = Math.Ceiling(project.ProgressTotal);
            return project;
        }
    }
}
