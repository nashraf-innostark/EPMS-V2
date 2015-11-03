﻿using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class ReportSearchRequest : GetPagedListRequest
    {
        public string Requester { get; set; }
        public ReportByColumn RequestByColumn
        {
            get
            {
                return (ReportByColumn)iSortCol_0;
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
