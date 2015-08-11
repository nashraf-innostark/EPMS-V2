using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Employee = new WebsiteModels.Employee();
            Allowance = new WebsiteModels.Allowance();
            OldAllowance = new WebsiteModels.Allowance();
            SearchRequest = new EmployeeSearchRequset();
            JobHistories = new List<WebsiteModels.EmployeeJobHistory>();
        }
        public WebsiteModels.Employee Employee { get; set; }
        public IList<WebsiteModels.EmployeeJobHistory> JobHistories { get; set; }
        public IEnumerable<WebsiteModels.Employee> EmployeeList { get; set; }
        public WebsiteModels.Allowance Allowance { get; set; }
        public WebsiteModels.Allowance OldAllowance { get; set; }

        public IEnumerable<EPMS.Models.DomainModels.Department> DepartmentList { get; set; }
        public IEnumerable<EPMS.Models.DomainModels.JobTitle> JobTitleList { get; set; }
        public IEnumerable<WebsiteModels.JobTitleAndDepartment> JobTitleDeptList { get; set; }
        public IEnumerable<WebsiteModels.Employee> aaData { get; set; }
        
        public EmployeeSearchRequset SearchRequest { get; set; }

        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        
        public string sEcho;


        /// <summary>
        /// Search Request
        /// </summary>
        //public EmployeeSearchRequset EmployeeSearchRequest { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }
        public string EmployeeName { get; set; }
        public string BtnText { get; set; }
        public string PageTitle { get; set; }
        public double Deduction1 { get; set; }
        public double Deduction2 { get; set; }

    }
}