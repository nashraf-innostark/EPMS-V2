﻿namespace EPMS.Models.DashboardModels
{
    public class IRFWidget
    {
        public long Id { get; set; }
        public string FormNumber { get; set; }
        public string RequesterName { get; set; }
        public string RequesterNameShort { get; set; }
        public int Status { get; set; }
        public string RecCreatedDate { get; set; }
    }
}