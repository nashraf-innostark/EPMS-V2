using System;

namespace EPMS.Web.Models
{
    public class Allowance
    {
        public long AllowanceId { get; set; }
        public long EmployeeId { get; set; }
        public string AllowanceDesc1 { get; set; }
        public double? Allowance1 { get; set; }
        public string AllowanceDesc2 { get; set; }
        public double? Allowance2 { get; set; }
        public string AllowanceDesc3 { get; set; }
        public double? Allowance3 { get; set; }
        public string AllowanceDesc4 { get; set; }
        public double? Allowance4 { get; set; }
        public string AllowanceDesc5 { get; set; }
        public double? Allowance5 { get; set; }
        public int RowVersion { get; set; }
        public DateTime? AllowanceDate { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual Employee Employee { get; set; }
    }
}