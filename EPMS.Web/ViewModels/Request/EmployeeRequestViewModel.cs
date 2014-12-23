using System.Collections.Generic;
using EPMS.Models.RequestModels;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Request
{
    public class EmployeeRequestViewModel
    {
        public EmployeeRequestViewModel()
        {
            EmployeeRequest = new EmployeeRequest();
            EmployeeRequestDetail = new RequestDetail();
        }
        //EmployeeRequest's Search Request data
        public EmployeeRequestSearchRequest SearchRequest { get; set; }
        public EmployeeRequest EmployeeRequest { get; set; }
        public IEnumerable<EmployeeRequest> EmployeeRequests { get; set; }
        public RequestDetail EmployeeRequestDetail { get; set; }
        public string RequestDescription { get; set; }
        public IEnumerable<EmployeeRequest> aaData { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;

        public int sEcho;
    }
}