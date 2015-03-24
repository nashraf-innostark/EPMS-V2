using System.Collections.Generic;

namespace EPMS.Web.ViewModels.Complaint
{
    public class ComplaintViewModel
    {
        public ComplaintViewModel()
        {
            Complaint=new Models.Complaint();
        }
        public Models.Complaint Complaint { get; set; }
        public IEnumerable<Models.Complaint> Complaints { get; set; }
        public IEnumerable<Models.Department> Departments { get; set; }
        public IEnumerable<Models.Order> Orders { get; set; }
        public long DeptId { get; set; }
        public long OdrId { get; set; }
    }
}