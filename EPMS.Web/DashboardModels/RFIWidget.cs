using System;

namespace EPMS.Web.DashboardModels
{
    public class RFIWidget
    {
        public long RFIId { get; set; }
        public string RequesterName { get; set; }
        public int Status { get; set; }
        public string RecCreatedDate { get; set; }
    }
}