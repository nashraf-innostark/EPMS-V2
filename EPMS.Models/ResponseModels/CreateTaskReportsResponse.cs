using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class CreateTaskReportsResponse
    {
        public IList<ProjectTask> Tasks { get; set; }
        public IList<DomainModels.Project> Projects { get; set; }
    }
}
