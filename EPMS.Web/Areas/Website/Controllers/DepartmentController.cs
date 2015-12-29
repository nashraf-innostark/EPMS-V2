using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers.Website.Department;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Website.Department;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class DepartmentController : BaseController
    {
        #region Private

        private readonly IWebsiteDepartmentService departmentService;
        
        #endregion

        #region Constructor
        public DepartmentController(IWebsiteDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        #endregion

        #region Public
        // GET: Website/Department
        [SiteAuthorize(PermissionKey = "WebsiteDepartmentCreate,WebsiteDepartmentView")]
        public ActionResult Create(long? id)
        {
            DepartmentViewModel viewModel = new DepartmentViewModel
            {
                WebsiteDepartment = id != null ? departmentService.FindDepartmentById((long)id).CreateFromServerToClient() : new WebsiteDepartment()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [SiteAuthorize(PermissionKey = "WebsiteDepartmentCreate")]
        public ActionResult Create(DepartmentViewModel viewModel)
        {
            try
            {
                if (Request.Form["Update"] != null)
                {
                    viewModel.WebsiteDepartment.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.WebsiteDepartment.RecUpdatedDate = DateTime.Now;
                    EPMS.Models.DomainModels.WebsiteDepartment departmentToUpdate = viewModel.WebsiteDepartment.CreateFromClientToServer();
                    if (departmentService.UpdateDepartment(departmentToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.Department.Department.UpdateMessage,
                            IsUpdated = true
                        };
                    }
                }
                else if (Request.Form["Delete"] != null)
                {
                    try
                    {
                        var directory = ConfigurationManager.AppSettings["DepartmentImage"];
                        var path = "~" + directory + viewModel.WebsiteDepartment.ImageName;
                        var fullPath = Request.MapPath(path);
                        Utility.DeleteFile(fullPath);
                        departmentService.DeleteDepartment(viewModel.WebsiteDepartment.DepartmentId);
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.Department.Department.DeleteMessage,
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
                    viewModel.WebsiteDepartment.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.WebsiteDepartment.RecCreatedDate = DateTime.Now;
                    viewModel.WebsiteDepartment.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.WebsiteDepartment.RecUpdatedDate = DateTime.Now;
                    EPMS.Models.DomainModels.WebsiteDepartment departmentToAdd = viewModel.WebsiteDepartment.CreateFromClientToServer();
                    if (departmentService.AddDepartment(departmentToAdd))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.Website.Department.Department.AddMessage,
                            IsSaved = true
                        };
                    }
                }
                return RedirectToAction("Index", "Home", new { Area = "Website" });
            }
            catch (Exception)
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = WebModels.Resources.Website.Department.Department.ErrorMessage,
                    IsError = true
                };
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
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["DepartmentImage"]);
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

        #region Delete Department

        public JsonResult DeleteDepartment(long departmentId, string imageName)
        {
            try
            {
                // Delete image
                if (!string.IsNullOrEmpty(imageName))
                {
                    var directory = ConfigurationManager.AppSettings["DepartmentImage"];
                    var path = "~" + directory + imageName;
                    var fullPath = Request.MapPath(path);
                    Utility.DeleteFile(fullPath);
                }
                // Delete data from DB
                departmentService.DeleteDepartment(departmentId);
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