using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.Employee
{
    public class EmployeeAjaxViewModel
    {
        /// <summary>
        /// To draw table
        /// </summary>
        private int draw = 1;

        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int recordsTotal;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int recordsFiltered;

        /// <summary>
        /// Data
        /// </summary>
        public IEnumerable<WebsiteModels.Employee> data;
        
        /// <summary>
        /// Search Request
        /// </summary>
        public EmployeeSearchRequset EmployeeSearchRequest { get; set; }
    }
}