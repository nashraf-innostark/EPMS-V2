using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class JobTitle
    {
        public long JobTitleId { get; set; }
        public string JobTitleNameE { get; set; }
        public string JobTitleDescE { get; set; }
        public string JobTitleNameA { get; set; }
        public string JobTitleDescA { get; set; }
        public long DepartmentId { get; set; }
        public double? BasicSalary { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<JobOffered> JobOffereds { get; set; }
    }
}
