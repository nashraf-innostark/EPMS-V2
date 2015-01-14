using System;

namespace EPMS.Web.Models
{
    public class JobOffered
    {
        public long JobOfferedId { get; set; }
        public long JobTitleId { get; set; }
        public string TitleE { get; set; }
        public string TitleA { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public bool ShowBasicSalary { get; set; }
        public bool IsOpen { get; set; }
        public string IsOpenStatus { get; set; }
        public int? NoOfPosts { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }

        public string JobTitleDescE { get; set; }
        public string JobTitleDescA { get; set; }
        public double? BasicSalary { get; set; }
        public string JobTitleNameE { get; set; }
        public string JobTitleNameA { get; set; }
    }
}