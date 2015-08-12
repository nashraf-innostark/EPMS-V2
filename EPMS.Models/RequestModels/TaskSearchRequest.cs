using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class TaskSearchRequest : GetPagedListRequest
    {
        public bool AllowedAll { get; set; }
        public string UserId { get; set; }
        public TaskByColumn TaskByColumn
        {
            get
            {
                return (TaskByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
