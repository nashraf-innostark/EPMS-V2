using System;

namespace EPMS.Web.Models
{
    public class PreRequisitTask
    {
        public long PreReqTaskId { get; set; }
        public long TaskId { get; set; }
        public long PreReqTask { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
}