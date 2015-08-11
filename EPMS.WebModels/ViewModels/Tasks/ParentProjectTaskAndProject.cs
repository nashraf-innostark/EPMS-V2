using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Tasks
{
    public class ParentProjectTaskAndProject
    {
        public IEnumerable<WebsiteModels.ProjectTask> ProjectTasks { get; set; }
        public IEnumerable<WebsiteModels.ProjectTask> ParentTasks { get; set; }
    }
}
