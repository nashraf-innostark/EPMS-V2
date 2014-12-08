using System;
using System.Web;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class EmployeeSearchRequset : GetPagedListRequest
    {
        public Guid UserId { get; set; }
        public long EmployeeId { get; set; }
        public string EmpFirstNameE { get; set; }
        public string EmpFirstNameA { get; set; }
        public string EmpMiddleNameE { get; set; }
        public string EmpMiddleNameA { get; set; }
        public string EmpLastNameE { get; set; }
        public string EmpLastNameA { get; set; }
        public long JobId { get; set; }
        public long departmentId { get; set; }
        public string EmpImage { get; set; }
        public string EmpMobileNumber { get; set; }
        public string EmpLandlineNumber { get; set; }
        public string EmpMaritalStatus { get; set; }
        public DateTime EmpDateOfBirth { get; set; }
        public string Nationality { get; set; }
        public long EmpIqama { get; set; }
        public DateTime IqamaIssueDate { get; set; }
        public DateTime IqamaExpiryDate { get; set; }
        public long PassportId { get; set; }
        public DateTime PassportExpiryDate { get; set; }
        public string ExtraInfo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public HttpPostedFileBase UploadImage { get; set; }

        public EmployeeByColumn EmployeeByColumn
        {
            get
            {
                return (EmployeeByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
