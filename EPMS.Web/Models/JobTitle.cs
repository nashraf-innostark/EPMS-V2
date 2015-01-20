using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace EPMS.Web.Models
{
    public class JobTitle
    {
        public long JobTitleId { get; set; }
        [Required(ErrorMessage = "Job Title is required.")]
        [StringLength(200, ErrorMessage = "Job Title Name cannot exceed 200 characters. ")]
        public string JobTitleNameE { get; set; }
        [Required(ErrorMessage = "Job Title is required.")]
        [StringLength(200, ErrorMessage = "Job Title Name cannot exceed 200 characters. ")]
        public string JobTitleNameA { get; set; }
        [StringLength(1000, ErrorMessage = "Description cannot exceed 200 characters. ")]
        public string JobTitleDescE { get; set; }
        [StringLength(1000, ErrorMessage = "Description cannot exceed 200 characters. ")]
        public string JobTitleDescA { get; set; }
        [Required(ErrorMessage = "Department is Required")]
        public long DepartmentId { get; set; }
        [Integer(ErrorMessage = "Basic Salary needs to be number")]
        public double BasicSalary { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }
        public long EmployeesCount { get; set; }

        //public Department Department { get; set; }
        //public ICollection<Employee> Employees { get; set; }
        //public ICollection<JobOffered> JobOffereds { get; set; }
    }
}