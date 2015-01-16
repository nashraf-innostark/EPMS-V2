using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.CompanyProfile
{
    public class CompanyProfileViewModel
    {
        public CompanyProfileViewModel()
        {
            CompanyProfile = new Models.CompanyProfile();
            CompanyDocuments = new CompanyDocumentDetail();
            CompanyBank = new CompanyBankDetail();
            CompanySocial = new CompanySocialDetail();
        }
        public Models.CompanyProfile CompanyProfile { get; set; }
        public CompanyDocumentDetail CompanyDocuments { get; set; }
        public CompanyBankDetail CompanyBank { get; set; }
        public CompanySocialDetail CompanySocial { get; set; }
        public int TabId { get; set; }
    }
}