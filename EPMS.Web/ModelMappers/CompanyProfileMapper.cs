﻿using System;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.CompanyProfile;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class CompanyProfileMapper
    {
        public static CompanyProfileViewModel CreateFromServerToClient(this DomainModels.CompanyProfile source)
        {
            CompanyProfileViewModel companyProfileViewModel = new CompanyProfileViewModel();

            companyProfileViewModel.CompanyProfile.CompanyId = source.CompanyId;
            companyProfileViewModel.CompanyProfile.CompanyLogoPath = source.CompanyLogoPath;
            companyProfileViewModel.CompanyProfile.CompanyNameE = source.CompanyNameE;
            companyProfileViewModel.CompanyProfile.CompanyNameA = source.CompanyNameA;
            companyProfileViewModel.CompanyProfile.CompanyWebsite = source.CompanyWebsite;
            companyProfileViewModel.CompanyProfile.CompanyEmail = source.CompanyEmail;
            companyProfileViewModel.CompanyProfile.CompanyAddressE = source.CompanyAddressE;
            companyProfileViewModel.CompanyProfile.CompanyAddressA = source.CompanyAddressA;
            companyProfileViewModel.CompanyProfile.CompanyLocation = source.CompanyLocation;
            companyProfileViewModel.CompanyProfile.CompanyNumber = source.CompanyNumber;
            companyProfileViewModel.CompanyProfile.CompanyMobileNumber = source.CompanyMobileNumber;
            companyProfileViewModel.CompanyProfile.RecCreatedBy = source.RecCreatedBy;
            companyProfileViewModel.CompanyProfile.RecCreatedDate = source.RecCreatedDate;
            companyProfileViewModel.CompanyProfile.RecLastUpdatedBy = source.RecLastUpdatedBy;
            companyProfileViewModel.CompanyProfile.RecLastUpdatedDate = source.RecLastUpdatedDate;
            if (source.CompanyDocumentDetail != null)
            {
                companyProfileViewModel.CompanyDocuments.CompanyId = source.CompanyId;
                companyProfileViewModel.CompanyDocuments.CommercialRegister = source.CompanyDocumentDetail.CommercialRegister;
                companyProfileViewModel.CompanyDocuments.CommercialRegisterIssueDate = source.CompanyDocumentDetail.CommercialRegisterIssueDate.ToString();
                companyProfileViewModel.CompanyDocuments.CommercialRegisterExpiryDate = source.CompanyDocumentDetail.CommercialRegisterExpiryDate.ToString();
                companyProfileViewModel.CompanyDocuments.InsuranceCertificate = source.CompanyDocumentDetail.InsuranceCertificate;
                companyProfileViewModel.CompanyDocuments.InsuranceCertificateIssueDate = source.CompanyDocumentDetail.InsuranceCertificateIssueDate.ToString();
                companyProfileViewModel.CompanyDocuments.InsuranceCertificateExpiryDate = source.CompanyDocumentDetail.InsuranceCertificateExpiryDate.ToString();
                companyProfileViewModel.CompanyDocuments.ChamberCertificate = source.CompanyDocumentDetail.ChamberCertificate;
                companyProfileViewModel.CompanyDocuments.ChamberCertificateIssueDate = source.CompanyDocumentDetail.ChamberCertificateIssueDate.ToString();
                companyProfileViewModel.CompanyDocuments.ChamberCertificateExpiryDate = source.CompanyDocumentDetail.ChamberCertificateExpiryDate.ToString();
                companyProfileViewModel.CompanyDocuments.IncomeAndZakaCertificate = source.CompanyDocumentDetail.IncomeAndZakaCertificate;
                companyProfileViewModel.CompanyDocuments.IncomeAndZakaCertificateIssueDate = source.CompanyDocumentDetail.IncomeAndZakaCertificateIssueDate.ToString();
                companyProfileViewModel.CompanyDocuments.IncomeAndZakaCertificateExpiryDate = source.CompanyDocumentDetail.IncomeAndZakaCertificateExpiryDate.ToString();
                companyProfileViewModel.CompanyDocuments.SaudilizationCertificate = source.CompanyDocumentDetail.SaudilizationCertificate;
                companyProfileViewModel.CompanyDocuments.SaudilizationCertificateIssueDate = source.CompanyDocumentDetail.SaudilizationCertificateIssueDate.ToString();
                companyProfileViewModel.CompanyDocuments.SaudilizationCertificateExpiryDate = source.CompanyDocumentDetail.SaudilizationCertificateExpiryDate.ToString();
            }
            if (source.CompanyBankDetail != null)
            {
                companyProfileViewModel.CompanyBank.CompanyId = source.CompanyId;
                companyProfileViewModel.CompanyBank.Bank1Name = source.CompanyBankDetail.Bank1Name;
                companyProfileViewModel.CompanyBank.Bank1NameArabic = source.CompanyBankDetail.Bank1NameArabic;
                companyProfileViewModel.CompanyBank.Bank1AccountNumber = source.CompanyBankDetail.Bank1AccountNumber;
                companyProfileViewModel.CompanyBank.Bank1IBANNumber = source.CompanyBankDetail.Bank1IBANNumber;
                companyProfileViewModel.CompanyBank.Bank1MobileNumber = source.CompanyBankDetail.Bank1MobileNumber;
                companyProfileViewModel.CompanyBank.Bank2Name = source.CompanyBankDetail.Bank2Name;
                companyProfileViewModel.CompanyBank.Bank2NameArabic = source.CompanyBankDetail.Bank2NameArabic;
                companyProfileViewModel.CompanyBank.Bank2AccountNumber = source.CompanyBankDetail.Bank2AccountNumber;
                companyProfileViewModel.CompanyBank.Bank2IBANNumber = source.CompanyBankDetail.Bank2IBANNumber;
                companyProfileViewModel.CompanyBank.Bank2MobileNumber = source.CompanyBankDetail.Bank2MobileNumber;
                companyProfileViewModel.CompanyBank.Bank3Name = source.CompanyBankDetail.Bank3Name;
                companyProfileViewModel.CompanyBank.Bank3NameArabic = source.CompanyBankDetail.Bank3NameArabic;
                companyProfileViewModel.CompanyBank.Bank3AccountNumber = source.CompanyBankDetail.Bank3AccountNumber;
                companyProfileViewModel.CompanyBank.Bank3IBANNumber = source.CompanyBankDetail.Bank3IBANNumber;
                companyProfileViewModel.CompanyBank.Bank3MobileNumber = source.CompanyBankDetail.Bank3MobileNumber;
                companyProfileViewModel.CompanyBank.Bank4Name = source.CompanyBankDetail.Bank4Name;
                companyProfileViewModel.CompanyBank.Bank4NameArabic = source.CompanyBankDetail.Bank4NameArabic;
                companyProfileViewModel.CompanyBank.Bank4AccountNumber = source.CompanyBankDetail.Bank4AccountNumber;
                companyProfileViewModel.CompanyBank.Bank4IBANNumber = source.CompanyBankDetail.Bank4IBANNumber;
                companyProfileViewModel.CompanyBank.Bank4MobileNumber = source.CompanyBankDetail.Bank4MobileNumber;
            }

            if (source.CompanySocialDetail != null)
            {

                companyProfileViewModel.CompanySocial.CompanyId = source.CompanyId;
                companyProfileViewModel.CompanySocial.Twitter = source.CompanySocialDetail.Twitter;
                companyProfileViewModel.CompanySocial.TwitterUserName = source.CompanySocialDetail.TwitterUserName;
                companyProfileViewModel.CompanySocial.TwitterPassword= source.CompanySocialDetail.TwitterPassword;
                companyProfileViewModel.CompanySocial.Facebook = source.CompanySocialDetail.Facebook;
                companyProfileViewModel.CompanySocial.FacebookUserName = source.CompanySocialDetail.FacebookUserName;
                companyProfileViewModel.CompanySocial.FacebookPassword = source.CompanySocialDetail.FacebookPassword;
                companyProfileViewModel.CompanySocial.Youtube = source.CompanySocialDetail.Youtube;
                companyProfileViewModel.CompanySocial.YoutubeUserName = source.CompanySocialDetail.YoutubeUserName;
                companyProfileViewModel.CompanySocial.YoutubePassword = source.CompanySocialDetail.YoutubePassword;
                companyProfileViewModel.CompanySocial.LinkedIn = source.CompanySocialDetail.LinkedIn;
                companyProfileViewModel.CompanySocial.LinkedInUserName = source.CompanySocialDetail.LinkedInUserName;
                companyProfileViewModel.CompanySocial.LinkedInPassword = source.CompanySocialDetail.LinkedInPassword;
                companyProfileViewModel.CompanySocial.GooglePlus = source.CompanySocialDetail.GooglePlus;
                companyProfileViewModel.CompanySocial.GooglePlusUserName = source.CompanySocialDetail.GooglePlusUserName;
                companyProfileViewModel.CompanySocial.GooglePlusPassword = source.CompanySocialDetail.GooglePlusPassword;
                companyProfileViewModel.CompanySocial.Istegram = source.CompanySocialDetail.Istegram;
                companyProfileViewModel.CompanySocial.IstegramUserName = source.CompanySocialDetail.IstegramUserName;
                companyProfileViewModel.CompanySocial.IstegramPassword = source.CompanySocialDetail.IstegramPassword;
                companyProfileViewModel.CompanySocial.Tumbler = source.CompanySocialDetail.Tumbler;
                companyProfileViewModel.CompanySocial.TumblerUserName = source.CompanySocialDetail.TumblerUserName;
                companyProfileViewModel.CompanySocial.TumblerPassword= source.CompanySocialDetail.TumblerPassword;
                companyProfileViewModel.CompanySocial.Flickr = source.CompanySocialDetail.Flickr;
                companyProfileViewModel.CompanySocial.FlickrUserName = source.CompanySocialDetail.FlickrUserName;
                companyProfileViewModel.CompanySocial.FlickrPassword = source.CompanySocialDetail.FlickrPassword;
                companyProfileViewModel.CompanySocial.Pinterest = source.CompanySocialDetail.Pinterest;
                companyProfileViewModel.CompanySocial.PinterestUserName = source.CompanySocialDetail.PinterestUserName;
                companyProfileViewModel.CompanySocial.PinterestPassword = source.CompanySocialDetail.PinterestPassword;
                companyProfileViewModel.CompanySocial.Social1 = source.CompanySocialDetail.Social1;
                companyProfileViewModel.CompanySocial.Social1UserName = source.CompanySocialDetail.Social1UserName;
                companyProfileViewModel.CompanySocial.Social1Password = source.CompanySocialDetail.Social1Password;
                companyProfileViewModel.CompanySocial.Social2 = source.CompanySocialDetail.Social2;
                companyProfileViewModel.CompanySocial.Social2UserName = source.CompanySocialDetail.Social2UserName;
                companyProfileViewModel.CompanySocial.Social2Password = source.CompanySocialDetail.Social2Password;
            }

            return companyProfileViewModel;
        }

        public static CompanyProfile CreateFromServerToClientForQuotation(this DomainModels.CompanyProfile source)
        {
            return new CompanyProfile
            {
                CompanyId = source.CompanyId,
                CompanyLogoPath = source.CompanyLogoPath,
                CompanyNameE = source.CompanyNameE,
                CompanyNameA = source.CompanyNameA,
                CompanyWebsite = source.CompanyWebsite,
                CompanyEmail = source.CompanyEmail,
                CompanyAddressE = source.CompanyAddressE,
                CompanyAddressA = source.CompanyAddressA,
                CompanyLocation = source.CompanyLocation,
                CompanyNumber = source.CompanyNumber,
                CompanyMobileNumber = source.CompanyMobileNumber,
            };
        }
        public static DomainModels.CompanyProfile CreateFromProfile(this Models.CompanyProfile source)
        {
            return new DomainModels.CompanyProfile
            {
                CompanyId = source.CompanyId,
                CompanyLogoPath = source.CompanyLogoPath,
                CompanyNameE = source.CompanyNameE,
                CompanyNameA = source.CompanyNameA,
                CompanyWebsite = source.CompanyWebsite,
                CompanyEmail = source.CompanyEmail,
                CompanyAddressE = source.CompanyAddressE,
                CompanyAddressA = source.CompanyAddressA,
                CompanyLocation = source.CompanyLocation,
                CompanyNumber = source.CompanyNumber,
                CompanyMobileNumber = source.CompanyMobileNumber,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }
        public static DomainModels.CompanyDocumentDetail CreateFromDocument(this Models.CompanyDocumentDetail source)
        {
            return new DomainModels.CompanyDocumentDetail
            {
                CompanyId = source.CompanyId,
                CommercialRegister = source.CommercialRegister,
                CommercialRegisterIssueDate = Convert.ToDateTime(source.CommercialRegisterIssueDate),
                CommercialRegisterExpiryDate = Convert.ToDateTime(source.CommercialRegisterExpiryDate),
                InsuranceCertificate = source.InsuranceCertificate,
                InsuranceCertificateIssueDate = Convert.ToDateTime(source.InsuranceCertificateIssueDate),
                InsuranceCertificateExpiryDate = Convert.ToDateTime(source.InsuranceCertificateExpiryDate),
                ChamberCertificate = source.ChamberCertificate,
                ChamberCertificateIssueDate = Convert.ToDateTime(source.ChamberCertificateIssueDate),
                ChamberCertificateExpiryDate = Convert.ToDateTime(source.ChamberCertificateExpiryDate),
                IncomeAndZakaCertificate = source.IncomeAndZakaCertificate,
                IncomeAndZakaCertificateIssueDate = Convert.ToDateTime(source.IncomeAndZakaCertificateIssueDate),
                IncomeAndZakaCertificateExpiryDate = Convert.ToDateTime(source.IncomeAndZakaCertificateExpiryDate),
                SaudilizationCertificate = source.SaudilizationCertificate,
                SaudilizationCertificateIssueDate = Convert.ToDateTime(source.SaudilizationCertificateIssueDate),
                SaudilizationCertificateExpiryDate = Convert.ToDateTime(source.SaudilizationCertificateExpiryDate),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }
        public static DomainModels.CompanyBankDetail CreateFromBank(this Models.CompanyBankDetail source)
        {
            return new DomainModels.CompanyBankDetail
            {
                CompanyId = source.CompanyId,
                Bank1Name = source.Bank1Name,
                Bank1NameArabic = source.Bank1NameArabic,
                Bank1AccountNumber = source.Bank1AccountNumber,
                Bank1IBANNumber = source.Bank1IBANNumber,
                Bank1MobileNumber = source.Bank1MobileNumber,
                Bank2Name = source.Bank2Name,
                Bank2NameArabic = source.Bank2NameArabic,
                Bank2AccountNumber = source.Bank2AccountNumber,
                Bank2IBANNumber = source.Bank2IBANNumber,
                Bank2MobileNumber = source.Bank2MobileNumber,
                Bank3Name = source.Bank3Name,
                Bank3NameArabic = source.Bank3NameArabic,
                Bank3AccountNumber = source.Bank3AccountNumber,
                Bank3IBANNumber = source.Bank3IBANNumber,
                Bank3MobileNumber = source.Bank3MobileNumber,
                Bank4Name = source.Bank4Name,
                Bank4NameArabic = source.Bank4NameArabic,
                Bank4AccountNumber = source.Bank4AccountNumber,
                Bank4IBANNumber = source.Bank4IBANNumber,
                Bank4MobileNumber = source.Bank4MobileNumber,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }
        public static DomainModels.CompanySocialDetail CreateFromSocial(this Models.CompanySocialDetail source)
        {
            return new DomainModels.CompanySocialDetail
            {
                CompanyId = source.CompanyId,
                Twitter = source.Twitter,
                TwitterUserName = source.TwitterUserName,
                TwitterPassword = source.TwitterPassword,
                Facebook = source.Facebook,
                FacebookUserName = source.FacebookUserName,
                FacebookPassword = source.FacebookPassword,
                Youtube = source.Youtube,
                YoutubeUserName = source.YoutubeUserName,
                YoutubePassword = source.YoutubePassword,
                LinkedIn = source.LinkedIn,
                LinkedInUserName = source.LinkedInUserName,
                LinkedInPassword = source.LinkedInPassword,
                GooglePlus = source.GooglePlus,
                GooglePlusUserName = source.GooglePlusUserName,
                GooglePlusPassword = source.GooglePlusPassword,
                Istegram = source.Istegram,
                IstegramUserName = source.IstegramUserName,
                IstegramPassword = source.IstegramPassword,
                Tumbler = source.Tumbler,
                TumblerUserName = source.TumblerUserName,
                TumblerPassword = source.TumblerPassword,
                Flickr = source.Flickr,
                FlickrUserName = source.FlickrUserName,
                FlickrPassword = source.FlickrPassword,
                Pinterest = source.Pinterest,
                PinterestUserName = source.PinterestUserName,
                PinterestPassword = source.PinterestPassword,
                Social1 = source.Social1,
                Social1UserName = source.Social1UserName,
                Social1Password = source.Social1Password,
                Social2 = source.Social2,
                Social2UserName = source.Social2UserName,
                Social2Password = source.Social2Password,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }
    }
}