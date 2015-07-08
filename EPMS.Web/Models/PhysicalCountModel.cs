namespace EPMS.Web.Models
{
    public class PhysicalCountModel
    {
        public long PCId { get; set; }
       
        public string RecCreatedByName { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
    }
}