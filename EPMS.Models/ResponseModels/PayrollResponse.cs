using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class PayrollResponse
    {
        public PayrollResponse()
        {
            Employee = new Employee();
            Allowance = new Allowance();
        }
        public Employee Employee { get; set; }
        public Allowance Allowance { get; set; }
        public IEnumerable<EmployeeRequest> Requests { get; set; }
 
    }
}
