using System;

namespace EPMS.Web.DashboardModels
{
    public class DIFWidget
    {
        public long Id { get; set; }
        public string FormNumber { get; set; }
        public string RequesterName { get; set; }
        public string RequesterNameShort { get; set; }
        public int Status { get; set; }
        public string RecCreatedDate { get; set; }
    }
}