using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class JobOfferedResponse
    {
        public JobOfferedResponse()
        {
            JobsOffered = new List<JobOffered>();
        }

        public IEnumerable<JobOffered> JobsOffered { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
