using System.Collections;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ViewModels.Project
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
            Project=new Models.Project();
        }
        public Models.Project Project { get; set; }
        public IEnumerable<Models.Customer> Customers { get; set; }
        public IEnumerable<Models.Order> Orders { get; set; }
        public IEnumerable<Models.ProjectTask> ProjectTasks { get; set; }
        public IEnumerable<ProjectDocument> ProjectDocsNames { get; set; }
        public Models.Quotation Installment { get; set; }
        public string DocsNames { get; set; }
    }
}