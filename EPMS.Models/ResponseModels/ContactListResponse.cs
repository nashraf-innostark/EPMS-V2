using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ContactListResponse
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<JobApplicant> JobApplicants { get; set; }
    }
}
