using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
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

        public ProjectTask ProjectTask { get; set; }
    }
}
