using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ComplaintResponse
    {
        public Complaint Complaint { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Department> Departments { get; set; }

    }
}
