using System;

namespace EPMS.Models.DomainModels
{
    public class PreRequisitTask
    {
        public long TaskId { get; set; }
        public long PreReqTask { get; set; }

        public virtual ProjectTask ProjectTask { get; set; }
    }
}
