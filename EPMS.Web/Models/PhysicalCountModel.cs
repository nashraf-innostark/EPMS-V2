using System;

namespace EPMS.Web.Models
{
    public class PhysicalCountModel
    {
        public long PCId { get; set; }
        public string RequesterEmpId { get; set; }
        public string RequesterName { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
    }
}