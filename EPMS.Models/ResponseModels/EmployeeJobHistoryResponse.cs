using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class EmployeeJobHistoryResponse
    {
        public EmployeeJobHistoryResponse()
        {
            EmployeeJobHistory = new List<EmployeeJobHistory>();
            JobTitleHistory = new List<JobTitleHistory>();
        }

        public IList<EmployeeJobHistory> EmployeeJobHistory { get; set; }
        public IList<JobHistory> EmployeeJobHistories { get; set; }
        public IList<JobTitleHistory> JobTitleHistory { get; set; }
        public IList<Allowance> PrevAllowances { get; set; }

    }
}
