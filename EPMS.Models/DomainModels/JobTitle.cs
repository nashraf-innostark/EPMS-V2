using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class JobTitle
    {
        public long JobId { get; set; }
        public string JobTitleNameE { get; set; }
        public string JobTitleNameA { get; set; }
        public string JobDescriptionE { get; set; }
        public string JobDescriptionA { get; set; }
        public Nullable<decimal> BasicSalary { get; set; }
        public long DepartmentId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
