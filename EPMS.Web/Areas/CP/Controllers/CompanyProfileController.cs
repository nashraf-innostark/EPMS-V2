using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.CompanyProfile;
using EPMS.WebModels.WebsiteModels;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.Web.Models;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.CompanyProfile;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CP.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "CP", IsModule = true)]
    public class CompanyProfileController : BaseController
    {
        #region Private

        private readonly ICompanyProfileService profileService;
        private readonly ICompanyDocumentService documentService;
        private readonly ICompanyBankService bankService;
        private readonly ICompanySocialService socialService;
        private readonly IEmployeeService employeeService;
        private readonly ICustomerService customerService;
        private readonly IJobApplicantService jobApplicantService;

        #endregion

        #region Constructor

        public CompanyProfileController(ICompanyProfileService profileService, ICompanyDocumentService documentService,
            ICompanyBankService bankService, ICompanySocialService socialService, IEmployeeService employeeService,
            ICustomerService customerService, IJobApplicantService jobApplicantService)
        {
            this.profileService = profileService;
            this.documentService = documentService;
            this.bankService = bankService;
            this.socialService = socialService;
            this.employeeService = employeeService;
            this.customerService = customerService;
            this.jobApplicantService = jobApplicantService;
        }

        #endregion

        #region Public

        [SiteAuthorize(PermissionKey = "CP")]
        public ActionResult Detail()
        {
            CompanyProfileViewModel companyProfileViewModel = new CompanyProfileViewModel();

            var companyProfile = profileService.GetDetail();
            if (companyProfile != null)
            {
                companyProfileViewModel = companyProfile.CreateFromServerToClient();
                ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] +
                                   companyProfileViewModel.CompanyProfile.CompanyLogoPath;
                ViewBag.CompanyId = companyProfileViewModel.CompanyProfile.CompanyId;
            }
            var contactList = new List<ContactList>();
            var empList =
                employeeService.GetAll()
                    .Where(x => !string.IsNullOrEmpty(x.EmployeeMobileNum))
                    .Select(x => x.CreateForContactList());
            var customerList =
                customerService.GetAll()
                    .Where(x => !string.IsNullOrEmpty(x.CustomerMobile))
                    .Select(x => x.CreateForContactList());
            var applicantList =
                jobApplicantService.GetAll()
                    .Where(x => !string.IsNullOrEmpty(x.MobileNumber))
                    .Select(x => x.CreateForContactList());
            contactList.AddRange(empList);
            contactList.AddRange(customerList);
            contactList.AddRange(applicantList);
            companyProfileViewModel.List = contactList;
            // Get license Information
            //var licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            //var licenseKey = EncryptDecrypt.StringCipher.Decrypt(licenseKeyEncrypted, "123");
            //var splitLicenseKey = licenseKey.Split('|');
            //companyProfileViewModel.LicenseInformation.ProductNo = splitLicenseKey[5];
            //companyProfileViewModel.LicenseInformation.NoOfUsers = splitLicenseKey[2];
            //companyProfileViewModel.LicenseInformation.LicenseNo = splitLicenseKey[6];
            //companyProfileViewModel.LicenseInformation.StartDate = splitLicenseKey[7];
            //companyProfileViewModel.LicenseInformation.ExpiryDate = splitLicenseKey[3];
            //companyProfileViewModel.LicenseInformation.HelpUrl = "";
            //prod no = 5, lic no = 6, start date = 7
            return View(companyProfileViewModel);
        }

        [HttpPost]
        [SiteAuthorize(PermissionKey = "CP")]
        public ActionResult Detail(CompanyProfileViewModel companyProfileViewModel)
        {
            if (companyProfileViewModel.TabId == 1)
            {
                companyProfileViewModel.CompanyProfile.RecLastUpdatedBy = User.Identity.GetUserId();
                companyProfileViewModel.CompanyProfile.RecLastUpdatedDate = DateTime.Now;
                var profileToUpdate = companyProfileViewModel.CompanyProfile.CreateFromProfile();
                if (profileService.UpdateDetail(profileToUpdate))
                {
                    TempData["message"] = new MessageViewModel { Message = WebModels.Resources.CP.Profile.RecordUpdated, IsUpdated = true };
                    return RedirectToAction("Detail");
                }
            }
            else if (companyProfileViewModel.TabId == 2)
            {
                companyProfileViewModel.CompanyDocuments.RecLastUpdatedBy = User.Identity.GetUserId();
                companyProfileViewModel.CompanyDocuments.RecLastUpdatedDate = DateTime.Now;
                var documentToUpdate = companyProfileViewModel.CompanyDocuments.CreateFromDocument();
                if (documentService.UpdateDetail(documentToUpdate))
                {
                    TempData["message"] = new MessageViewModel { Message = WebModels.Resources.CP.Profile.RecordUpdated, IsUpdated = true };
                    return RedirectToAction("Detail");
                }
            }
            else if (companyProfileViewModel.TabId == 4)
            {
                companyProfileViewModel.CompanyBank.RecLastUpdatedBy = User.Identity.GetUserId();
                companyProfileViewModel.CompanyBank.RecLastUpdatedDate = DateTime.Now;
                var bankToUpdate = companyProfileViewModel.CompanyBank.CreateFromBank();
                if (bankService.UpdateDetail(bankToUpdate))
                {
                    TempData["message"] = new MessageViewModel { Message = WebModels.Resources.CP.Profile.RecordUpdated, IsUpdated = true };
                    return RedirectToAction("Detail");
                }
            }
            else if (companyProfileViewModel.TabId == 5)
            {
                companyProfileViewModel.CompanySocial.RecLastUpdatedBy = User.Identity.GetUserId();
                companyProfileViewModel.CompanySocial.RecLastUpdatedDate = DateTime.Now;
                var socialToUpdate = companyProfileViewModel.CompanySocial.CreateFromSocial();
                if (socialService.UpdateDetail(socialToUpdate))
                {
                    TempData["message"] = new MessageViewModel { Message = WebModels.Resources.CP.Profile.RecordUpdated, IsUpdated = true };
                    return RedirectToAction("Detail");
                }
            }
            return View(companyProfileViewModel);
        }

        public ActionResult UploadLogo()
        {
            HttpPostedFileBase companyLogo = Request.Files[0];
            var filename = "";
            try
            {
                //Save File to Folder
                if ((companyLogo != null))
                {
                    filename =
                        (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + companyLogo.FileName)
                            .Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["CompanyLogo"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    companyLogo.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return
                    Json(
                        new
                        {
                            response = "Failed to upload. Error: " + exp.Message,
                            status = (int)HttpStatusCode.BadRequest
                        }, JsonRequestBehavior.AllowGet);
            }
            return
                Json(
                    new
                    {
                        filename = filename,
                        size = companyLogo.ContentLength / 1024 + "KB",
                        response = "Successfully uploaded!",
                        status = (int)HttpStatusCode.OK
                    }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendSms(string bankName, string bankNameAr, string bankAccountNo, string bankIbanNo, string mobileNum)
        {
            string smsText = "Bank Name: " + bankName + ", Bank Name Arabic: " + bankNameAr + 
                ", Account #: " + bankAccountNo + ", Iban #: " + bankIbanNo;
            string mobileNo = mobileNum;
            // Send SMS
            bool smsResponse = Utility.SendSms(smsText, mobileNo);
            return Json(smsResponse);
        }
        #endregion
    }
}