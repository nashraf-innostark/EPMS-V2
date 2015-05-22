using System.Collections.Generic;

namespace EPMS.Web.ViewModels.Tasks
{
    public class ParentProjectTaskAndProject
    {
        public IEnumerable<Models.ProjectTask> ProjectTasks { get; set; }
        public IEnumerable<Models.ProjectTask> ParentTasks { get; set; }
    }
}
