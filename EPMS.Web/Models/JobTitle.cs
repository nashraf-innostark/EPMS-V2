using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace EPMS.Web.Models
{
    public class JobTitle
    {
        public JobTitle()
        {
            Department=new Department();
        }
        public long JobTitleId { get; set; }
        [Required(ErrorMessage = "Job Title is required.")]
        public string JobTitleNameE { get; set; }
        public string JobTitleDescE { get; set; }
        public string JobTitleNameA { get; set; }
        public string JobTitleDescA { get; set; }
        [Required(ErrorMessage = "Department is Required")]
        public long DepartmentId { get; set; }
        [Integer(ErrorMessage = "Basic Salary needs to be number")]
        public double? BasicSalary { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public Department Department { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<JobOffered> JobOffereds { get; set; }
    }
}