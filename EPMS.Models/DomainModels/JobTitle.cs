using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class JobTitle
    {
        public long JobTitleId { get; set; }
        public string JobTitleName { get; set; }
        public string JobTitleDesc { get; set; }
        public long DepartmentId { get; set; }
        public Nullable<double> BasicSalary { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<JobOffered> JobOffereds { get; set; }
    }
}
