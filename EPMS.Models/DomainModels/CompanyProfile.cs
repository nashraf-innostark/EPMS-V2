using System;

namespace EPMS.Models.DomainModels
{
    public class CompanyProfile
    {
        public long CompanyId { get; set; }
        public string CompanyNameE { get; set; }
        public string CompanyNameA { get; set; }
        public string CompanyLogoPath { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddressE { get; set; }
        public string CompanyAddressA { get; set; }
        public string CompanyLocation { get; set; }
        public string CompanyNumber { get; set; }
        public string CompanyMobileNumber { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }

        public virtual CompanyBankDetail CompanyBankDetail { get; set; }
        public virtual CompanySocialDetail CompanySocialDetail { get; set; }
        public virtual CompanyDocumentDetail CompanyDocumentDetail { get; set; }
    }
}
