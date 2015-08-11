using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ViewModels.Project
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
            Project=new WebsiteModels.Project();
        }
        public WebsiteModels.Project Project { get; set; }
        public IEnumerable<WebsiteModels.Customer> Customers { get; set; }
        public IEnumerable<WebsiteModels.Order> Orders { get; set; }
        public IEnumerable<WebsiteModels.Quotation> Quotations { get; set; }
        public IEnumerable<WebsiteModels.ProjectTask> ProjectTasks { get; set; }
        public IEnumerable<ProjectDocument> ProjectDocsNames { get; set; }
        public WebsiteModels.Quotation Installment { get; set; }
        public string DocsNames { get; set; }
    }
}