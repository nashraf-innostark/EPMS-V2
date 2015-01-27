using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class TaskSearchRequest : GetPagedListRequest
    {
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
