using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Tasks
{
    public class TaskCreateViewModel
    {
        public TaskCreateViewModel()
        {
            ProjectTask = new ProjectTask();
            RequisitTasks = new List<PreRequisitTask>();
        }
        public ProjectTask ProjectTask { get; set; }
        public IList<PreRequisitTask> RequisitTasks { get; set; }
        public string PageTitle { get; set; }
        public string BtnText { get; set; }
        public string Header { get; set; }
    }
}