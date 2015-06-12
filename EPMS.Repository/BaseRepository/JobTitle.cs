//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EPMS.Repository.BaseRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobTitle
    {
        public JobTitle()
        {
            this.Employees = new HashSet<Employee>();
            this.EmployeeJobHistories = new HashSet<EmployeeJobHistory>();
            this.JobOffereds = new HashSet<JobOffered>();
            this.JobTitleHistories = new HashSet<JobTitleHistory>();
        }
    
        public long JobTitleId { get; set; }
        public string JobTitleNameE { get; set; }
        public string JobTitleDescE { get; set; }
        public long DepartmentId { get; set; }
        public Nullable<double> BasicSalary { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }
        public string JobTitleNameA { get; set; }
        public string JobTitleDescA { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<EmployeeJobHistory> EmployeeJobHistories { get; set; }
        public virtual ICollection<JobOffered> JobOffereds { get; set; }
        public virtual ICollection<JobTitleHistory> JobTitleHistories { get; set; }
    }
}
