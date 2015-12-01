using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Customer
    {
        public long CustomerId { get; set; }
        public string CustomerNameE { get; set; }
        public string CustomerNameA { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public long? EmployeeId { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
        public virtual ICollection<RFQ> RFQs { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
