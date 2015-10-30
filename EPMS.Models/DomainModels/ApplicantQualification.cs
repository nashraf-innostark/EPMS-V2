﻿using System;

namespace EPMS.Models.DomainModels
{
    public class ApplicantQualification
    {
        public long QualificationId { get; set; }
        public long? ApplicantId { get; set; }
        public string Certificate { get; set; }
        public string Field { get; set; }
        public string PlaceOfStudy { get; set; }
        public string CollegeSchoolName { get; set; }
        public string NoOfYears { get; set; }
        public DateTime? CertificateDate { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual JobApplicant JobApplicant { get; set; }
    }
}
