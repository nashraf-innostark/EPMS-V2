using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class EmployeeRequestResponse
    {
        public EmployeeRequestResponse()
        {
            EmployeeRequests = new List<EmployeeRequest>();
        }
        public IEnumerable<EmployeeRequest> EmployeeRequests { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
