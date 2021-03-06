﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;

namespace EPMS.Web.Areas.HR.Models
{
    public class JobTitle
    {
        public long JobTitleId { get; set; }
        [Required(ErrorMessage = "Job Title is required.")]
        public string JobTitleName { get; set; }
        [Required(ErrorMessage = "Job description is required.")]
        public string JobTitleDesc { get; set; }
        public long DepartmentId { get; set; }
        [Integer(ErrorMessage = "Basic Salary needs to be number")]
        public double? BasicSalary { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<JobOffered> JobOffereds { get; set; }
    }
}