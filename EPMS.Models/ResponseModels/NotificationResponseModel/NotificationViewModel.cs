using System.Collections.Generic;
using EPMS.Models.ResponseModels.EmployeeResponseModel;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationViewModel
    {
        public NotificationViewModel()
        {
            NotificationResponse=new NotificationResponse();
        }
        public IEnumerable<EmployeeDDL> EmployeeDDL { get; set; }
        public NotificationResponse NotificationResponse { get; set; }
    }
}
