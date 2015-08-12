using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class CompanyBankDetail
    {
        public long CompanyId { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot exceed 500 characters.")]
        public string Bank1Name { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot exceed 500 characters.")]
        public string Bank1NameArabic { get; set; }
        [StringLength(500, ErrorMessage = "Account Number cannot exceed 500 characters.")]
        public string Bank1AccountNumber { get; set; }
        [StringLength(500, ErrorMessage = "IBAN Number cannot exceed 500 characters.")]
        public string Bank1IBANNumber { get; set; }
        [StringLength(500, ErrorMessage = "Mobile Number cannot exceed 100 characters.")]
        [Required]
        [Display(Name = "Bank Mobile Number")]
        public string Bank1MobileNumber { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot exceed 500 characters.")]
        public string Bank2Name { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot exceed 500 characters.")]
        public string Bank2NameArabic { get; set; }
        [StringLength(500, ErrorMessage = "Account Number cannot exceed 500 characters.")]
        public string Bank2AccountNumber { get; set; }
        [StringLength(500, ErrorMessage = "IBAN Number cannot exceed 500 characters.")]
        public string Bank2IBANNumber { get; set; }
        [StringLength(500, ErrorMessage = "Mobile Number cannot exceed 100 characters.")]
        [Required]
        [Display(Name = "Bank Mobile Number")]
        public string Bank2MobileNumber { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot exceed 500 characters.")]
        public string Bank3Name { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot exceed 500 characters.")]
        public string Bank3NameArabic { get; set; }
        [StringLength(500, ErrorMessage = "Account Number cannot exceed 500 characters.")]
        public string Bank3AccountNumber { get; set; }
        [StringLength(500, ErrorMessage = "IBAN Number cannot exceed 500 characters.")]
        public string Bank3IBANNumber { get; set; }
        [StringLength(500, ErrorMessage = "Mobile Number cannot exceed 100 characters.")]
        [Required]
        [Display(Name = "Bank Mobile Number")]
        public string Bank3MobileNumber { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot exceed 500 characters.")]
        public string Bank4Name { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot exceed 500 characters.")]
        public string Bank4NameArabic { get; set; }
        [StringLength(500, ErrorMessage = "Account Number cannot exceed 500 characters.")]
        public string Bank4AccountNumber { get; set; }
        [StringLength(500, ErrorMessage = "IBAN Number cannot exceed 500 characters.")]
        public string Bank4IBANNumber { get; set; }
        [StringLength(500, ErrorMessage = "Mobile Number cannot exceed 100 characters.")]
        [Required]
        [Display(Name = "Bank Mobile Number")]
        public string Bank4MobileNumber { get; set; }
        public string CompanyLogoPath { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
    }
}