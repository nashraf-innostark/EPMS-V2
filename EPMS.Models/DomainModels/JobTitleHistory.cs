namespace EPMS.Models.DomainModels
{
    public class JobTitleHistory
    {
        public long Id { get; set; }
        public long JobTitleId { get; set; }
        public double BasicSalary { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }

        public virtual JobTitle JobTitle { get; set; }
    }
}
