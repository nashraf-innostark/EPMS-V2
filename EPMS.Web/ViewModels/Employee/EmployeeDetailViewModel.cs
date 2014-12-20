using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.Employee
{
    public class EmployeeDetailViewModel
    {
        public Models.Employee Employee { get; set; }

        public Models.JobTitle JobTitle { get; set; }
        public Models.Allowance Allowance { get; set; }

        public IEnumerable<EPMS.Models.DomainModels.JobTitle> JobTitleList { get; set; }
    }
}