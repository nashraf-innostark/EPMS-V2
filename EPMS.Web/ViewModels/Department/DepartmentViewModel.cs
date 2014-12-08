using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public Models.Department Department { get; set; }
        public IEnumerable<Models.Department> DepartmentList { get; set; }
        
    }
}