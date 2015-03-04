using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class CompanyProfile
    {
        public CompanyProfile()
        {
            Documents=new CompanyDocumentDetail();
            Bank=new CompanyBankDetail();
            Social=new CompanySocialDetail();
        }
        public long CompanyId { get; set; }
        [StringLength(500, ErrorMessage = "Company Name cannot exceed 500 characters.")]
        public string CompanyNameE { get; set; }
        [StringLength(500, ErrorMessage = "Company Name cannot exceed 500 characters.")]
        public string CompanyNameA { get; set; }
        public string CompanyLogoPath { get; set; }
        [StringLength(500, ErrorMessage = "Website cannot exceed 200 characters.")]
        public string CompanyWebsite { get; set; }
        [StringLength(500, ErrorMessage = "Email cannot exceed 200 characters.")]
        [EmailAddress]
        public string CompanyEmail { get; set; }
        [StringLength(500, ErrorMessage = "Address cannot exceed 1000 characters.")]
        public string CompanyAddressE { get; set; }
        [StringLength(500, ErrorMessage = "Address cannot exceed 1000 characters.")]
        public string CompanyAddressA { get; set; }
        [StringLength(500, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string CompanyLocation { get; set; }
        [StringLength(500, ErrorMessage = "Number cannot exceed 200 characters.")]
        public string CompanyNumber { get; set; }
        [StringLength(500, ErrorMessage = "Mobile Number cannot exceed 200 characters.")]
        public string CompanyMobileNumber { get; set; }
        public string CommercialRegister { get; set; }
        public string CommercialRegisterIssueDate { get; set; }
        public string CommercialRegisterIssueDateAr { get; set; }
        public string CommercialRegisterExpiryDate { get; set; }
        public string CommercialRegisterExpiryDateAr { get; set; }
        public string InsuranceCertificate { get; set; }
        public string InsuranceCertificateIssueDate { get; set; }
        public string InsuranceCertificateIssueDateAr { get; set; }
        public string InsuranceCertificateExpiryDate { get; set; }
        public string InsuranceCertificateExpiryDateAr { get; set; }
        public string ChamberCertificate { get; set; }
        public string ChamberCertificateIssueDate { get; set; }
        public string ChamberCertificateIssueDateAr { get; set; }
        public string ChamberCertificateExpiryDate { get; set; }
        public string ChamberCertificateExpiryDateAr { get; set; }
        public string IncomeAndZakaCertificate { get; set; }
        public string IncomeAndZakaCertificateIssueDate { get; set; }
        public string IncomeAndZakaCertificateIssueDateAr { get; set; }
        public string IncomeAndZakaCertificateExpiryDate { get; set; }
        public string IncomeAndZakaCertificateExpiryDateAr { get; set; }
        public string SaudilizationCertificate { get; set; }
        public string SaudilizationCertificateIssueDate { get; set; }
        public string SaudilizationCertificateIssueDateAr { get; set; }
        public string SaudilizationCertificateExpiryDate { get; set; }
        public string SaudilizationCertificateExpiryDateAr { get; set; }
        public string Bank1Name { get; set; }
        public string Bank1NameArabic { get; set; }
        public string Bank1AccountNumber { get; set; }
        public string Bank1IBANNumber { get; set; }
        public string Bank1MobileNumber { get; set; }
        public string Bank2Name { get; set; }
        public string Bank2NameArabic { get; set; }
        public string Bank2AccountNumber { get; set; }
        public string Bank2IBANNumber { get; set; }
        public string Bank2MobileNumber { get; set; }
        public string Bank3Name { get; set; }
        public string Bank3NameArabic { get; set; }
        public string Bank3AccountNumber { get; set; }
        public string Bank3IBANNumber { get; set; }
        public string Bank3MobileNumber { get; set; }
        public string Bank4Name { get; set; }
        public string Bank4NameArabic { get; set; }
        public string Bank4AccountNumber { get; set; }
        public string Bank4IBANNumber { get; set; }
        public string Bank4MobileNumber { get; set; }
        public string Twitter { get; set; }
        public string TwitterUserName { get; set; }
        public string TwitterPassword { get; set; }
        public string Facebook { get; set; }
        public string FacebookUserName { get; set; }
        public string FacebookPassword { get; set; }
        public string Youtube { get; set; }
        public string YoutubeUserName { get; set; }
        public string YoutubePassword { get; set; }
        public string LinkedIn { get; set; }
        public string LinkedInUserName { get; set; }
        public string LinkedInPassword { get; set; }
        public string GooglePlus { get; set; }
        public string GooglePlusUserName { get; set; }
        public string GooglePlusPassword { get; set; }
        public string Istegram { get; set; }
        public string IstegramUserName { get; set; }
        public string IstegramPassword { get; set; }
        public string Tumbler { get; set; }
        public string TumblerUserName { get; set; }
        public string TumblerPassword { get; set; }
        public string Flickr { get; set; }
        public string FlickrUserName { get; set; }
        public string FlickrPassword { get; set; }
        public string Pinterest { get; set; }
        public string PinterestUserName { get; set; }
        public string PinterestPassword { get; set; }
        public string Social1 { get; set; }
        public string Social1UserName { get; set; }
        public string Social1Password { get; set; }
        public string Social2 { get; set; }
        public string Social2UserName { get; set; }
        public string Social2Password { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
        public CompanyDocumentDetail Documents { get; set; }
        public CompanySocialDetail Social { get; set; }
        public CompanyBankDetail Bank { get; set; }
    }
}