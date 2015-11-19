using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class TaskEmployee
    {
        public long TaskEmployeeId { get; set; }
        public long TaskId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string EmployeeNameEn { get; set; }
        public string EmployeeNameAr { get; set; }

        public ProjectTask ProjectTask { get; set; }
    }
}