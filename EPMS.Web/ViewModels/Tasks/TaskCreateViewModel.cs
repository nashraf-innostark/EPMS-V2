using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Tasks
{
    public class TaskCreateViewModel
    {
        public ProjectTask ProjectTask { get; set; }
        public string PageTitle { get; set; }
        public string BtnText { get; set; }
        public string Header { get; set; }
    }
}