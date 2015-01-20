using System;
using System.Configuration;
using System.Globalization;
using System.IO;
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

        #endregion
        
        #region Constructor

        public CompanyProfileController(ICompanyProfileService profileService, ICompanyDocumentService documentService, ICompanyBankService bankService, ICompanySocialService socialService)
        {
            this.profileService = profileService;
            this.documentService = documentService;
            this.bankService = bankService;
            this.socialService = socialService;
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

        public ActionResult SendSms()
        {
            try
            {
                var companyProfile = profileService.GetDetail();
                CompanyProfileViewModel companyProfileViewModel = companyProfile.CreateFromServerToClient();

                string username = ConfigurationManager.AppSettings["MobileUsername"];
                string password = ConfigurationManager.AppSettings["MobilePassword"];
                string senderId = ConfigurationManager.AppSettings["SenderID"];
                string smsText = "Bank Name: " + companyProfileViewModel.BankName + " Bank Name Arabic: " +
                                 companyProfileViewModel.BankNameAr + " Account # " +
                                 companyProfileViewModel.BankAccountNo + " Iban # " + companyProfileViewModel.BankIbanNo +
                                 " Mobile #" + companyProfileViewModel.BankMobileNo;
                string mobileNo = "abc";
                WebRequest smsRequest =
                    WebRequest.Create("http://www.jawalbsms.ws/api.php/sendsms?user=" + username + "&pass=" +
                                      password +
                                      "&to=" + mobileNo + "&message=" + smsText +
                                      "&sender=" + senderId);
                WebResponse smsRequestResponse = smsRequest.GetResponse();
                Stream smsDataStream = smsRequestResponse.GetResponseStream();
                StreamReader smsReader = new StreamReader(smsDataStream);
                string smsResponse = smsReader.ReadToEnd();
                //Patient SMS End

                if (smsResponse.ToLower().Contains("success"))
                {
                    return Json(new { response = "", status = 200 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {

            }
            return Json(new {response = "", status = 500}, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}