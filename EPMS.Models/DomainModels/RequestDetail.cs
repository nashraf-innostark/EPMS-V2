using System;

namespace EPMS.Models.DomainModels
{
    public class RequestDetail
    {
        public long RequestDetailId { get; set; }
        public long RequestId { get; set; }
        public string RequestDesc { get; set; }
        public string RequestReply { get; set; }
        public double? LoanAmount { get; set; }
        public DateTime? LoanDate { get; set; }
        public double? InstallmentAmount { get; set; }
        public DateTime? FirstInstallmentDate { get; set; }
        public DateTime? LastInstallmentDate { get; set; }
        public int? NumberOfMonths { get; set; }
        public bool IsReplied { get; set; }
        public bool IsApproved { get; set; }
        public int RowVersion { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual EmployeeRequest EmployeeRequest { get; set; }
    }
}
