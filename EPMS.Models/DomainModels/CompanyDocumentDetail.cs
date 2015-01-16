using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class CompanyDocumentDetail
    {
        public long CompanyId { get; set; }
        public string CommercialRegister { get; set; }
        public DateTime? CommercialRegisterIssueDate { get; set; }
        public DateTime? CommercialRegisterExpiryDate { get; set; }
        public string InsuranceCertificate { get; set; }
        public DateTime? InsuranceCertificateIssueDate { get; set; }
        public DateTime? InsuranceCertificateExpiryDate { get; set; }
        public string ChamberCertificate { get; set; }
        public DateTime? ChamberCertificateIssueDate { get; set; }
        public DateTime? ChamberCertificateExpiryDate { get; set; }
        public string IncomeAndZakaCertificate { get; set; }
        public DateTime? IncomeAndZakaCertificateIssueDate { get; set; }
        public DateTime? IncomeAndZakaCertificateExpiryDate { get; set; }
        public string SaudilizationCertificate { get; set; }
        public DateTime? SaudilizationCertificateIssueDate { get; set; }
        public DateTime? SaudilizationCertificateExpiryDate { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }

        public virtual CompanyProfile CompanyProfile { get; set; }
    }
}
