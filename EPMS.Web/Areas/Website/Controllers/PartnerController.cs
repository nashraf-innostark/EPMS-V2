using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.Partner;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Partner;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Website.Controllers
{
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
                if (Request.Form["Update"] != null)
                {
                    viewModel.Partner.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.Partner.RecUpdatedDate = DateTime.Now;
                    EPMS.Models.DomainModels.Partner partnerToUpdate = viewModel.Partner.CreateFromClientToServer();
                    if (partnerService.UpdatePartner(partnerToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Partner Updated",
                            IsUpdated = true
                        };
                    }
                }
                else if (Request.Form["Delete"] != null)
                {
                    try
                    {
                        partnerService.DeletePartner(viewModel.Partner.PartnerId);
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Partner Deleted",
                            IsUpdated = true
                        };
                    }
                    catch (Exception)
                    {
                        return View(viewModel);
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
                            Message = "Partner Added",
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
    }
}