using System.Collections.Generic;

namespace EPMS.Models.ResponseModels
{
    public class ProjectResponseForDashboard
    {
        public Project Project { get; set; }
        public IEnumerable<ProjectTaskResponse> ProjectTasks { get; set; } 
    }
}
