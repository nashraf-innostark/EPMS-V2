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
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.CompanyProfile;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CP.Controllers
{
    [Authorize]
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

        public CompanyProfileController(ICompanyProfileService profileService, ICompanyDocumentService documentService, ICompanyBankService bankService, ICompanySocialService socialService, IEmployeeService employeeService, ICustomerService customerService, IJobApplicantService jobApplicantService)
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
                ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + companyProfileViewModel.CompanyProfile.CompanyLogoPath;
                ViewBag.CompanyId = companyProfileViewModel.CompanyProfile.CompanyId;
            }
            var contactList = new List<ContactList>();
            var empList = employeeService.GetAll().Where(x => !string.IsNullOrEmpty(x.EmployeeMobileNum)).Select(x => x.CreateForContactList());
            var customerList = customerService.GetAll().Where(x => !string.IsNullOrEmpty(x.CustomerMobile)).Select(x => x.CreateForContactList());
            var applicantList = jobApplicantService.GetAll().Where(x => !string.IsNullOrEmpty(x.ApplicantMobile)).Select(x => x.CreateForContactList());
            contactList.AddRange(empList);
            contactList.AddRange(customerList);
            contactList.AddRange(applicantList);
            companyProfileViewModel.List = contactList;
            return View(companyProfileViewModel);
        }
        [HttpPost]
        public ActionResult Detail(CompanyProfileViewModel companyProfileViewModel)
        {
            if (companyProfileViewModel.TabId == 1)
            {
                companyProfileViewModel.CompanyProfile.RecLastUpdatedBy = User.Identity.GetUserId();
                companyProfileViewModel.CompanyProfile.RecLastUpdatedDate = DateTime.Now;
                var profileToUpdate = companyProfileViewModel.CompanyProfile.CreateFromProfile();
                if (profileService.UpdateDetail(profileToUpdate))
                {
                    TempData["message"] = new MessageViewModel { Message = "Updated", IsUpdated = true };
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
                    TempData["message"] = new MessageViewModel { Message = "Updated", IsUpdated = true };
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
                    TempData["message"] = new MessageViewModel { Message = "Updated", IsUpdated = true };
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
                    TempData["message"] = new MessageViewModel { Message = "Updated", IsUpdated = true };
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
                    filename = (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + companyLogo.FileName).Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["CompanyLogo"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    companyLogo.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { filename = filename, size = companyLogo.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendSms(CompanyProfileViewModel companyProfileViewModel)
        {
            string username = ConfigurationManager.AppSettings["MobileUsername"];
            string password = ConfigurationManager.AppSettings["MobilePassword"];
            string senderId = ConfigurationManager.AppSettings["SenderID"];
            string smsText = "Bank Name: " + companyProfileViewModel.BankName + ", Bank Name Arabic: " +
                                companyProfileViewModel.BankNameAr + ", Account #: " +
                                companyProfileViewModel.BankAccountNo + ", Iban #: " + companyProfileViewModel.BankIbanNo +
                                ", Mobile #:" + companyProfileViewModel.BankMobileNo;
            string mobileNo = companyProfileViewModel.MobileNo.Aggregate("", (current, item) => current +","+ item);
            WebRequest smsRequest =
                WebRequest.Create("http://www.jawalbsms.ws/api.php/sendsms?user=" + username + "&pass=" +
                                    password +
                                    "&to=" + mobileNo.Substring(1, mobileNo.Length-1) + "&message=" + smsText +
                                    "&sender=" + senderId);
            WebResponse smsRequestResponse = smsRequest.GetResponse();
            Stream smsDataStream = smsRequestResponse.GetResponseStream();
            StreamReader smsReader = new StreamReader(smsDataStream);
            string smsResponse = smsReader.ReadToEnd();
            //Patient SMS End

            if (smsResponse.ToLower().Contains("success"))
            {
                TempData["message"] = new MessageViewModel {Message = "Message Sent", IsInfo = true};
                return RedirectToAction("Detail");
            }
            TempData["message"] = new MessageViewModel { Message = "Error Code " + smsResponse, IsError = true };
            return RedirectToAction("Detail");
                
            }
            

        

        #endregion
}