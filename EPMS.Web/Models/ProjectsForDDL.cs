using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.Models
{
    public class ProjectsForDDL
    {
        public long ProjectId { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ProjectTasksSum { get; set; }
    }
}