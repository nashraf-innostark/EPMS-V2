using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class Department
    {
        public long DepartmentId { get; set; }
        [Required(ErrorMessage = "Department Name is required.")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Department Description is required.")]
        public string DepartmentDesc { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual ICollection<JobTitle> JobTitles { get; set; }
    }
}