using System.Collections.Generic;

namespace EPMS.Web.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public Models.Department Department { get; set; }
        public IEnumerable<Models.Department> DepartmentList { get; set; }
        
    }
}