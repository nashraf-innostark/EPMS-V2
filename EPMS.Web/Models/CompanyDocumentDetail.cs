using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class CompanyDocumentDetail
    {
        public long CompanyId { get; set; }
        [StringLength(500, ErrorMessage = "Company Register Number cannot exceed 500 characters.")]
        public string CommercialRegister { get; set; }
        public DateTime? CommercialRegisterIssueDate { get; set; }
        public DateTime? CommercialRegisterIssueDateAr { get; set; }
        public DateTime? CommercialRegisterExpiryDate { get; set; }
        public DateTime? CommercialRegisterExpiryDateAr { get; set; }
        [StringLength(500, ErrorMessage = "Insurance Certificate Number cannot exceed 500 characters.")]
        public string InsuranceCertificate { get; set; }
        public DateTime? InsuranceCertificateIssueDate { get; set; }
        public DateTime? InsuranceCertificateIssueDateAr { get; set; }
        public DateTime? InsuranceCertificateExpiryDate { get; set; }
        public DateTime? InsuranceCertificateExpiryDateAr { get; set; }
        [StringLength(500, ErrorMessage = "Chamber Certificate Number cannot exceed 500 characters.")]
        public string ChamberCertificate { get; set; }
        public DateTime? ChamberCertificateIssueDate { get; set; }
        public DateTime? ChamberCertificateIssueDateAr { get; set; }
        public DateTime? ChamberCertificateExpiryDate { get; set; }
        public DateTime? ChamberCertificateExpiryDateAr { get; set; }
        [StringLength(500, ErrorMessage = "Income and Zaka Certificate Number cannot exceed 500 characters.")]
        public string IncomeAndZakaCertificate { get; set; }
        public DateTime? IncomeAndZakaCertificateIssueDate { get; set; }
        public DateTime? IncomeAndZakaCertificateIssueDateAr { get; set; }
        public DateTime? IncomeAndZakaCertificateExpiryDate { get; set; }
        public DateTime? IncomeAndZakaCertificateExpiryDateAr { get; set; }
        [StringLength(500, ErrorMessage = "Saudilization Certificate Number cannot exceed 500 characters.")]
        public string SaudilizationCertificate { get; set; }
        public DateTime? SaudilizationCertificateIssueDate { get; set; }
        public DateTime? SaudilizationCertificateIssueDateAr { get; set; }
        public DateTime? SaudilizationCertificateExpiryDate { get; set; }
        public DateTime? SaudilizationCertificateExpiryDateAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
    }
}