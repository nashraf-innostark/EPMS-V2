using System.Collections.Generic;

namespace EPMS.Models.ResponseModels
{
    public class ProjectResponseForDashboard
    {
        public string RoleName { get; set; }
        public Project Project { get; set; }
        public IEnumerable<ProjectTaskResponse> ProjectTasks { get; set; } 
    }
}
