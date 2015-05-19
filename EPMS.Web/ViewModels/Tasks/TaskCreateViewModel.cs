using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Tasks
{
    public class TaskCreateViewModel
    {
        public TaskCreateViewModel()
        {
            ProjectTask = new ProjectTask();
            RequisitTasks = new List<long>();
            AssignedEmployees = new List<long>();
            OldRequisitTasks = new List<long>();
            OldAssignedEmployees = new List<long>();
        }
        public ProjectTask ProjectTask { get; set; }
        public List<long> RequisitTasks { get; set; }
        public List<long> OldRequisitTasks { get; set; }
        public List<ProjectTask> PreRequisitTasks { get; set; }
        public List<long> AssignedEmployees { get; set; }
        public List<long> OldAssignedEmployees { get; set; }
        public IEnumerable<Models.Project> Projects { get; set; }
        public IEnumerable<ProjectsForDDL> ProjectsForDdls { get; set; }
        public IEnumerable<ProjectTask> ProjectAllTasks { get; set; }
        public IEnumerable<ProjectTask> AllParentTasks { get; set; }
        public IEnumerable<Models.Employee> AllEmployees { get; set; }
        public string PageTitle { get; set; }
        public string BtnText { get; set; }
        public string Header { get; set; }
    }
}