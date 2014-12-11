using System.Collections.Generic;
using EPMS.Models.RequestModels;
using AreasModel=EPMS.Web.Areas.HR.Models;

namespace EPMS.Web.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public AreasModel.Employee Employee { get; set; }

        public AreasModel.JobTitle JobTitle { get; set; }
        public AreasModel.Department Department { get; set; }

        public IEnumerable<EPMS.Models.DomainModels.Department> DepartmentList { get; set; }
        public IEnumerable<EPMS.Models.DomainModels.JobTitle> JobTitleList { get; set; }
        public IEnumerable<Models.JobTitleAndDepartment> JobTitleDeptList { get; set; }
        public IEnumerable<AreasModel.Employee> data { get; set; }

        public EmployeeSearchRequset SearchRequest { get; set; }

        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int recordsTotal;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int recordsFiltered;

        /// <summary>
        /// Search Request
        /// </summary>
        public EmployeeSearchRequset EmployeeSearchRequest { get; set; }
        public string FilePath { get; set; }
    }
}