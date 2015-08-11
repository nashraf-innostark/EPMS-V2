using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.CompanyProfile
{
    public class CompanyProfileViewModel
    {
        public CompanyProfileViewModel()
        {
            CompanyProfile = new WebsiteModels.CompanyProfile();
            CompanyDocuments = new WebsiteModels.CompanyDocumentDetail();
            CompanyBank = new WebsiteModels.CompanyBankDetail();
            CompanySocial = new WebsiteModels.CompanySocialDetail();
            LicenseInformation = new WebsiteModels.CompanyLicenseInformation();
        }
        public WebsiteModels.CompanyProfile CompanyProfile { get; set; }
        public WebsiteModels.CompanyDocumentDetail CompanyDocuments { get; set; }
        public WebsiteModels.CompanyBankDetail CompanyBank { get; set; }
        public WebsiteModels.CompanySocialDetail CompanySocial { get; set; }
        public WebsiteModels.CompanyLicenseInformation LicenseInformation { get; set; }
        public int TabId { get; set; }
        public string BankName { get; set; }
        public string BankNameAr { get; set; }
        public string BankAccountNo { get; set; }
        public string BankIbanNo { get; set; }
        public string BankMobileNo { get; set; }
        public List<string> MobileNo { get; set; }
        public IEnumerable<WebsiteModels.ContactList> List { get; set; }
    }
}