﻿using System;

namespace EPMS.Web.Models
{
    public class CompanyBankDetail
    {
        public long CompanyId { get; set; }
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
        public string CompanyLogoPath { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDate { get; set; }
    }
}