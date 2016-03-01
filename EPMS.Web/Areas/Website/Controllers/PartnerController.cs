using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.Partner;
using EPMS.WebModels.ViewModels.Partner;
using EPMS.WebModels.WebsiteModels;
using EPMS.Web.Controllers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Website.Controllers
{
    [SiteAuthorize(PermissionKey = "Website", IsModule = true)]
    public class PartnerController : BaseController
    {
        #region Private

        private readonly IPartnerService partnerService;
        
        #endregion

        #region Constructor
        public PartnerController(IPartnerService partnerService)
        {
            this.partnerService = partnerService;
        }

        #endregion

        #region Public

        // GET: Website/Partner
        [SiteAuthorize(PermissionKey = "PartnerCreate,PartnerView")]
        public ActionResult Create(long? id)
        {
            PartnerViewModel viewModel = new PartnerViewModel
            {
                Partner = id != null ? partnerService.FindPartnerById((long)id).CreateFromServerToClient() : new Partner()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [SiteAuthorize(PermissionKey = "PartnerCreate")]
        public ActionResult Create(PartnerViewModel viewModel)
        {
            try
            {
                if (viewModel.Partner.PartnerId > 0)
                {
                    viewModel.Partner.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Partner.RecUpdatedDate = DateTime.Now;
                    EPMS.Models.DomainModels.Partner partnerToUpdate = viewModel.Partner.CreateFromClientToServer();
                    if (partnerService.UpdatePartner(partnerToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.Partner.Partner.UpdateMessage,
                            IsUpdated = true
                        };
                    }
                }
                else
                {
                    viewModel.Partner.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.Partner.RecCreatedDate = DateTime.Now;
                    viewModel.Partner.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Partner.RecUpdatedDate = DateTime.Now;
                    EPMS.Models.DomainModels.Partner partnerToAdd = viewModel.Partner.CreateFromClientToServer();
                    if (partnerService.AddPartner(partnerToAdd))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.Partner.Partner.AddMessage,
                            IsUpdated = true
                        };
                    }
                }
                return RedirectToAction("Index", "Home", new { Area = "Website" });
            }
            catch (Exception)
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        [SiteAuthorize(PermissionKey = "DeletePartner")]
        public ActionResult Delete(long partnerId, string imageName)
        {
            try
            {
                var directory = ConfigurationManager.AppSettings["PartnerImage"];
                var path = "~" + directory + imageName;
                var fullPath = Request.MapPath(path);
                Utility.DeleteFile(fullPath);
                partnerService.DeletePartner(partnerId);
                TempData["message"] = new MessageViewModel
                {
                    Message = WebModels.Resources.Website.Partner.Partner.DeleteMessage,
                    IsUpdated = true
                };
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = WebModels.Resources.Website.Partner.Partner.DeleteErrorMessage,
                    IsUpdated = true
                };
                return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Save Image

        public ActionResult UploadImage()
        {
            HttpPostedFileBase userPhoto = Request.Files[0];
            try
            {
                string savedFileName = "";
                //Save image to Folder
                if ((userPhoto != null))
                {
                    var filename = userPhoto.FileName;
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["PartnerImage"]);
                    savedFileName = Path.Combine(filePathOriginal, filename);
                    userPhoto.SaveAs(savedFileName);
                    return Json(new { filename = userPhoto.FileName, size = userPhoto.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { response = "Failed to upload", status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Delete Partner

        public JsonResult DeletePartner(long partnerId, string imageName)
        {
            try
            {
                // Delete image
                if (!string.IsNullOrEmpty(imageName))
                {
                    var directory = ConfigurationManager.AppSettings["PartnerImage"];
                    var path = "~" + directory + imageName;
                    var fullPath = Request.MapPath(path);
                    Utility.DeleteFile(fullPath);
                }
                // Delete data from DB
                partnerService.DeletePartner(partnerId);
                return Json("Deleted", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}