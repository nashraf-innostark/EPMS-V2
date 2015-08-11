using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Complaint
{
    public class ComplaintViewModel
    {
        public ComplaintViewModel()
        {
            Complaint=new WebsiteModels.Complaint();
        }
        public WebsiteModels.Complaint Complaint { get; set; }
        public IEnumerable<WebsiteModels.Complaint> Complaints { get; set; }
        public IEnumerable<WebsiteModels.Department> Departments { get; set; }
        public IEnumerable<WebsiteModels.Order> Orders { get; set; }
        public long DeptId { get; set; }
        public long OdrId { get; set; }
    }
}