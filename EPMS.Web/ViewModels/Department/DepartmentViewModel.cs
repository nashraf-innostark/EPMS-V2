using System.Collections.Generic;
using AreasModel=EPMS.Web.Areas.HR.Models;

namespace EPMS.Web.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public AreasModel.Department Department { get; set; }
        public IEnumerable<AreasModel.Department> DepartmentList { get; set; }
        
    }
}