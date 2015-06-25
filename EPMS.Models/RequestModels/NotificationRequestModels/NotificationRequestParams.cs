using EPMS.Models.Common;

namespace EPMS.Models.RequestModels.NotificationRequestModels
{
    public class NotificationRequestParams
    {
        public string RoleName { get; set; }
        public long EmployeeId { get; set; }
        public long CustomerId { get; set; }
        public string UserId { get; set; }
        public bool SystemGenerated { get; set; }
        public int RoleId { get; set; }
        public int RoleKey { get; set; }
        public UserRole UserRoleKey
        {
            get
            {
                return (UserRole)RoleKey;
            }
            set
            {
                RoleKey = (short)value;
            }
        }
    }
}
