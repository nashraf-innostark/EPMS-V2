using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class DepartmentResponse
    {
        public DepartmentResponse()
        {
            Departments = new List<Department>();
        }

        public IEnumerable<Department> Departments { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
