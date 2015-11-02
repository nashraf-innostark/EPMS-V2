namespace EPMS.WebModels.WebsiteModels
{
    /// <summary>
    /// Applicant model used for Job Applicants List
    /// </summary>
    public class ApplicantModel
    {
        /// <summary>
        /// Job Applicant Id
        /// </summary>
        public long JobApplicantId { get; set; }
        /// <summary>
        /// Job Applicant Name English
        /// </summary>
        public string ApplicantNameEn { get; set; }
        /// <summary>
        /// Job Applicant Name Arabic
        /// </summary>
        public string ApplicantNameAr { get; set; }
        /// <summary>
        /// Job Applicant Email
        /// </summary>
        public string ApplicantEmail { get; set; }
        /// <summary>
        /// Job Applicant Mobile
        /// </summary>
        public string ApplicantMobile { get; set; }
        /// <summary>
        /// Job Offered (Job Title Name English)
        /// </summary>
        public string JobOfferedEn { get; set; }
        /// <summary>
        /// Job Offered (Job Title Name Arabic)
        /// </summary>
        public string JobOfferedAr { get; set; }
        /// <summary>
        /// Job Offering DepartmentEnglish
        /// </summary>
        public string DepartmentNameEn { get; set; }
        /// <summary>
        /// Job Offering Department Arabic
        /// </summary>
        public string DepartmentNameAr { get; set; }
    }
}