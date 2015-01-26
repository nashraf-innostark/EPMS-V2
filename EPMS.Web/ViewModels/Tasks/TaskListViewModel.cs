using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Tasks
{
    public class TaskListViewModel
    {
        public IEnumerable<Models.ProjectTask> aaData { get; set; }
        public TaskSearchRequest SearchRequest { get; set; }
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