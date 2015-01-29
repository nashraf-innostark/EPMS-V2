using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    public class MeetingRequest
    {
        public Meeting Meeting { get; set; }
        public List<long> EmployeeIds { get; set; }
        public List<long> AbsentEmployeeIds { get; set; }
        public string DocsNames { get; set; }
    }
}
