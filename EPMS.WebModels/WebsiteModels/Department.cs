using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class Department
    {
        public long DepartmentId { get; set; }
        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(200, ErrorMessage = "Cannot exceed 200 characters.")]
        public string DepartmentNameE { get; set; }
        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(200, ErrorMessage = "Cannot exceed 200 characters.")]
        public string DepartmentNameA { get; set; }
        [StringLength(1000, ErrorMessage = "Cannot exceed 1000 characters.")]
        public string DepartmentDesc { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        //public virtual ICollection<JobTitle> JobTitles { get; set; }
    }
}