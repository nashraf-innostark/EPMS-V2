using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Project
{
    public class ProjectListViewModel
    {
        public IEnumerable<WebsiteModels.Project> Projects { get; set; }
    }
}