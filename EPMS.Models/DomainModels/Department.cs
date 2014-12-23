using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Department
    {
        public long DepartmentId { get; set; }
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }
        public string DepartmentDesc { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual ICollection<JobTitle> JobTitles { get; set; }
    }
}
