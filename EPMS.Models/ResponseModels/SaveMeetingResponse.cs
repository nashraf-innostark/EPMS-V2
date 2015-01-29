using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.ResponseModels
{
    public sealed class SaveMeetingResponse
    {
        public IEnumerable<string> EmployeeEmails { get; set; } 
    }
}
