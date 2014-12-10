using System;
using System.Web;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class EmployeeSearchRequset : GetPagedListRequest
    {
        public Guid UserId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeMiddleName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeImagePath { get; set; }
        public Nullable<long> JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        public string EmployeeMobileNum { get; set; }
        public string EmployeeLandlineNum { get; set; }
        public Nullable<byte> MaritalStatus { get; set; }
        public Nullable<System.DateTime> EmployeeDOB { get; set; }
        public string EmpDateOfBirthArabic { get; set; }
        public Nullable<short> EmployeeNationality { get; set; }
        public Nullable<int> EmployeeIqama { get; set; }
        public Nullable<System.DateTime> EmployeeIqamaIssueDt { get; set; }
        public Nullable<System.DateTime> EmployeeIqamaExpiryDt { get; set; }
        public string EmployeePassportNum { get; set; }
        public Nullable<System.DateTime> EmployeePassportExpiryDt { get; set; }
        public string EmployeeDetails { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }

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
