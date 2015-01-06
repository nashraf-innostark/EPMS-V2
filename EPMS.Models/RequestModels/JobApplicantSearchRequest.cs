using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    /// <summary>
    /// Job Applicant Search Request Text and Column
    /// </summary>
    public class JobApplicantSearchRequest : GetPagedListRequest
    {
        public string SearchText { get; set; }
        public long JobApplicantId { get; set; }
        public string JobApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantMobile { get; set; }
        public string JobOffered { get; set; }
        public string Department { get; set; }
        public string sEcho { get; set; }
        public string sSearch { get; set; }
        public JobApplicantByColumn JobApplicantRequestByColumn
        {
            get
            {
                return (JobApplicantByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
