namespace EPMS.Web.Models
{
    public class RFI
    {
        public long RFIId { get; set; }
        public long CustomerId { get; set; }
        public long OrderId { get; set; }
        public string UsageE { get; set; }
        public string UsageA { get; set; }
        public string RecCreatedByName { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
    }
}