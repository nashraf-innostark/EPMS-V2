using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class RequestDetail
    {
        public long RequestDetailId { get; set; }
        public long RequestId { get; set; }
        [Display(Name = "Description")]
        public string RequestDesc { get; set; }
        [Display(Name="Loan Amount")]
        public double? LoanAmount { get; set; }
        [Display(Name = "Loan Date")]
        public DateTime? LoanDate { get; set; }
        [Display(Name = "Installment Amount")]
        public double? InstallmentAmount { get; set; }
        [Display(Name = "First Installment Date")]
        public DateTime? FirstInstallmentDate { get; set; }
        [Display(Name = "Last Installment Date")]
        public DateTime? LastInstallmentDate { get; set; }
        [Display(Name = "Number of Months")]
        public int? NumberOfMonths { get; set; }
        [Display(Name = "Replied")]
        public bool IsReplied { get; set; }
        [Display(Name = "Approved")]
        public bool IsApproved { get; set; }
        public int RowVersion { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual EmployeeRequest EmployeeRequest { get; set; }
    }
}