using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class Department
    {

        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }

        public virtual ICollection<JobTitle> JobTitles { get; set; }
    }
}
