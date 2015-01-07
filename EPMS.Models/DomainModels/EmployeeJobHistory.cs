namespace EPMS.Models.DomainModels
{
    public class EmployeeJobHistory
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long JobTitleId { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual JobTitle JobTitle { get; set; }
    }
}
