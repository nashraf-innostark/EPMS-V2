using System;
using System.Collections.Generic;

namespace EPMS.Web.Models
{
    public class JobOffered
    {
        public long JobOfferedId { get; set; }
        public long DepartmentId { get; set; }
        public string TitleE { get; set; }
        public string TitleA { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public long BasicSalary { get; set; }
        public bool IsOpen { get; set; }
        public int NoOfPosts { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public ICollection<JobApplicant> JobApplicants { get; set; }
        public Department Department { get; set; }
    }
}