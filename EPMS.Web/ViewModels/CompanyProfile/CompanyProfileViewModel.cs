﻿using System.Collections.Generic;
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
            LicenseInformation = new CompanyLicenseInformation();
        }
        public Models.CompanyProfile CompanyProfile { get; set; }
        public CompanyDocumentDetail CompanyDocuments { get; set; }
        public CompanyBankDetail CompanyBank { get; set; }
        public CompanySocialDetail CompanySocial { get; set; }
        public CompanyLicenseInformation LicenseInformation { get; set; }
        public int TabId { get; set; }
        public string BankName { get; set; }
        public string BankNameAr { get; set; }
        public string BankAccountNo { get; set; }
        public string BankIbanNo { get; set; }
        public string BankMobileNo { get; set; }
        public List<string> MobileNo { get; set; }
        public IEnumerable<ContactList> List { get; set; }
    }
}