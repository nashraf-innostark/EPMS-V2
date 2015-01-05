using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Request
{
    public class RequestListViewModel
    {
        //EmployeeRequest's Search Request data
        public EmployeeRequestSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// Requests List
        /// </summary>
        public IEnumerable<Models.Request> aaData { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;
        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        public string sEcho;
    }
}