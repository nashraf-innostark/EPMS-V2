using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class TaskResponse
    {
        public TaskResponse()
        {
            ProjectTasks = new List<ProjectTask>();
        }

        public IEnumerable<ProjectTask> ProjectTasks { get; set; }
        public ProjectTask ProjectTask { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<DomainModels.Project> Projects { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
