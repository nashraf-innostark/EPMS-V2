using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class CompanyDocumentDetail
    {
        public long CompanyId { get; set; }
        [StringLength(500, ErrorMessage = "Company Register Number cannot exceed 500 characters.")]
        public string CommercialRegister { get; set; }
        public string CommercialRegisterIssueDate { get; set; }
        public string CommercialRegisterIssueDateAr { get; set; }
        public string CommercialRegisterExpiryDate { get; set; }
        public string CommercialRegisterExpiryDateAr { get; set; }
        [StringLength(500, ErrorMessage = "Insurance Certificate Number cannot exceed 500 characters.")]
        public string InsuranceCertificate { get; set; }
        public string InsuranceCertificateIssueDate { get; set; }
        public string InsuranceCertificateIssueDateAr { get; set; }
        public string InsuranceCertificateExpiryDate { get; set; }
        public string InsuranceCertificateExpiryDateAr { get; set; }
        [StringLength(500, ErrorMessage = "Chamber Certificate Number cannot exceed 500 characters.")]
        public string ChamberCertificate { get; set; }
        public string ChamberCertificateIssueDate { get; set; }
        public string ChamberCertificateIssueDateAr { get; set; }
        public string ChamberCertificateExpiryDate { get; set; }
        public string ChamberCertificateExpiryDateAr { get; set; }
        [StringLength(500, ErrorMessage = "Income and Zaka Certificate Number cannot exceed 500 characters.")]
        public string IncomeAndZakaCertificate { get; set; }
        public string IncomeAndZakaCertificateIssueDate { get; set; }
        public string IncomeAndZakaCertificateIssueDateAr { get; set; }
        public string IncomeAndZakaCertificateExpiryDate { get; set; }
        public string IncomeAndZakaCertificateExpiryDateAr { get; set; }
        [StringLength(500, ErrorMessage = "Saudilization Certificate Number cannot exceed 500 characters.")]
        public string SaudilizationCertificate { get; set; }
        public string SaudilizationCertificateIssueDate { get; set; }
        public string SaudilizationCertificateIssueDateAr { get; set; }
        public string SaudilizationCertificateExpiryDate { get; set; }
        public string SaudilizationCertificateExpiryDateAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
    }
}