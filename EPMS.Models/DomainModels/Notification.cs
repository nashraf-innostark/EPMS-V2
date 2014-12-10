using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class Notification
    {
        public long NotificationId { get; set; }
        public string NotificationName { get; set; }
        public short NotificationCategory { get; set; }
        public System.DateTime AlertTime { get; set; }
        public Nullable<System.DateTime> NotificationTime { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public bool IsNotified { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
