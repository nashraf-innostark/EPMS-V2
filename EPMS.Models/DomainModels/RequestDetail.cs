using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class RequestDetail
    {
        public long RequestDetailId { get; set; }
        public long RequestId { get; set; }
        public string RequestDesc { get; set; }
        public Nullable<double> LoanAmount { get; set; }
        public Nullable<System.DateTime> LoanDate { get; set; }
        public Nullable<double> InstallmentAmount { get; set; }
        public Nullable<System.DateTime> FirstInstallmentDate { get; set; }
        public Nullable<System.DateTime> LastInstallmentDate { get; set; }
        public Nullable<int> NumberOfMonths { get; set; }
        public bool IsReplied { get; set; }
        public bool IsApproved { get; set; }
        public int RowVersion { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }

        public virtual EmployeeRequest EmployeeRequest { get; set; }
    }
}
