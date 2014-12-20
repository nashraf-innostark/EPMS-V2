using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public Models.Department Department { get; set; }
        public IEnumerable<Models.Department> DepartmentList { get; set; }
        public DepartmentSearchRequest SearchRequest { get; set; }
        
    }
}