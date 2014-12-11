using System.Collections.Generic;
using EPMS.Models.RequestModels;
using AreasModel=EPMS.Web.Areas.HR.Models;

namespace EPMS.Web.ViewModels.JobTitle
{
    public class JobTitleViewModel
    {
        public AreasModel.JobTitle JobTitle { get; set; }
        public AreasModel.Department Department { get; set; }
        public IEnumerable<AreasModel.JobTitle> JobTitleList { get; set; }
        public IEnumerable<AreasModel.Department> DepartmentList { get; set; }
        public JobTitleSearchRequest SearchRequest { get; set; }
    }
}