﻿using EPMS.Models.Common;

namespace EPMS.Models.RequestModels.NotificationRequestModels
{
    public class NotificationListViewRequest : GetPagedListRequest
    {
        public string RoleName { get; set; }
        public long EmployeeId { get; set; }
        public long CustomerId { get; set; }
        public string UserId { get; set; }
        public NotificationByColumn NotificationByColumn
        {
            get
            {
                return (NotificationByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
        private int _SortCol;
        public int iSortCol_0
        {
            get
            {
                return _SortCol;
            }
            set
            {
                _SortCol = value == 0 ? 0 : value;
            }
        }
    }
}
