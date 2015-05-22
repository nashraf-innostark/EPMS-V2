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
        public virtual ICollection<JobOffered> JobOffereds { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<JobApplicant> JobApplicants { get; set; }
    }
}
