using System;

namespace EPMS.Models.DomainModels
{
    public class Allowance
    {
        public long AllowanceId { get; set; }
        public long EmployeeId { get; set; }
        public string AllowanceDesc1 { get; set; }
        public Nullable<double> Allowance1 { get; set; }
        public string AllowanceDesc2 { get; set; }
        public Nullable<double> Allowance2 { get; set; }
        public string AllowanceDesc3 { get; set; }
        public Nullable<double> Allowance3 { get; set; }
        public string AllowanceDesc4 { get; set; }
        public Nullable<double> Allowance4 { get; set; }
        public string AllowanceDesc5 { get; set; }
        public Nullable<double> Allowance5 { get; set; }
        public int RowVersion { get; set; }
        public DateTime? AllowanceDate { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
