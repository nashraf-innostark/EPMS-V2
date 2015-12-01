using System.Collections.Generic;

namespace EPMS.Models.ResponseModels
{
    public class CustomerResponse
    {
        public DomainModels.Customer Customer { get; set; }
        public IEnumerable<DomainModels.Employee> Employees { get; set; }
    }
}
