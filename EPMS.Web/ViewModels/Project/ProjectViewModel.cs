namespace EPMS.Web.ViewModels.Project
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
            Project=new Models.Project();
        }
        public Models.Project Project { get; set; }
    }
}