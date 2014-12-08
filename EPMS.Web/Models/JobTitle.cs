using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;

namespace EPMS.Web.Models
{
    public class JobTitle
    {
        public long JobId { get; set; }
        [Required(ErrorMessage = "Job Title is required.")]
        public string JobTitleNameE { get; set; }
        public string JobTitleNameA { get; set; }
        [Required(ErrorMessage = "Job description is required.")]
        public string JobDescriptionE { get; set; }
        public string JobDescriptionA { get; set; }
        [Integer(ErrorMessage = "Basic Salary needs to be number")]
        public Nullable<decimal> BasicSalary { get; set; }
        public long DepartmentId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual Department Department { get; set; }
    }
}