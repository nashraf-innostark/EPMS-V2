using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Employee
{
    public class EmployeeViewModel
    {

        public Models.Employee Employee { get; set; }
        public Models.JobTitle JobTitle { get; set; }
        public Models.Allowance Allowance { get; set; }

        public IEnumerable<EPMS.Models.DomainModels.Department> DepartmentList { get; set; }
        public IEnumerable<EPMS.Models.DomainModels.JobTitle> JobTitleList { get; set; }
        public IEnumerable<Models.JobTitleAndDepartment> JobTitleDeptList { get; set; }
        public IEnumerable<Models.Employee> aaData { get; set; }
        
        public EmployeeSearchRequset SearchRequest { get; set; }

        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        
        public int sEcho;


        /// <summary>
        /// Search Request
        /// </summary>
        //public EmployeeSearchRequset EmployeeSearchRequest { get; set; }
        public string ImagePath { get; set; }
        public string EmployeeName { get; set; }
        public string BtnText { get; set; }
        public string PageTitle { get; set; }

    }
}