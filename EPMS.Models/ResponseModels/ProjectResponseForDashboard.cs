using System.Collections.Generic;

namespace EPMS.Models.ResponseModels
{
    public class ProjectResponseForDashboard
    {
        public Project Project { get; set; }
        public IEnumerable<ProjectTask> ProjectTasks { get; set; } 
    }
}
