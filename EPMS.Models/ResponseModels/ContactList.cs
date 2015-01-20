using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.ResponseModels
{
    public class ContactList
    {
        public string Link { get; set; }
        public long SerialNo { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public string Type { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
