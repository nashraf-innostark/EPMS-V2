using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    /// <summary>
    /// Job Applicant Search Request Text and Column
    /// </summary>
    public class JobApplicantSearchRequest : GetPagedListRequest
    {
        public string SearchText { get; set; }
        public JobApplicantByColumn JobApplicantRequestByColumn
        {
            get
            {
                return (JobApplicantByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
