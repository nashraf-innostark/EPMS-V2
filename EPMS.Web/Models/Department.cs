using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPMS.Web.Models
{
    public class Department
    {
        public long? DepartmentId { get; set; }
        [Required(ErrorMessage = "Department Name is required.")]
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }
        [Required(ErrorMessage = "Department Description is required.")]
        public string DepartmentDesc { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<JobTitle> JobTitles { get; set; }
    }
}