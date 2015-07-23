using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.EnumForDropDown;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Slider;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class SliderController : BaseController
    {
        #region Private

        private readonly IImageSliderService sliderService;

        #endregion

        #region Constructor
        public SliderController(IImageSliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        #endregion

        #region Public

        // GET: Website/Slider
        [SiteAuthorize(PermissionKey = "SliderCreate,SliderView")]
        public ActionResult Create(long? id)
        {
            SliderViewModel viewModel = new SliderViewModel
            {
                ImageSlider =
                    id != null
                        ? sliderService.FindImageSliderById((long) id).CreateFromServerToClient()
                        : new Models.ImageSlider()
            };
            IEnumerable<Position> positions = Enum.GetValues(typeof(Position))
                                                       .Cast<Position>();
            viewModel.Position = from action in positions
                                 select new SelectListItem
                                 {
                                     Text = action.ToString(),
                                     Value = ((int)action).ToString()
                                 };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SliderViewModel viewModel)
        {
            try
            {
                if (Request.Form["Update"] != null)
                {
                    viewModel.ImageSlider.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.ImageSlider.RecUpdatedDate = DateTime.Now;
                    ImageSlider imageSliderToUpdate = viewModel.ImageSlider.CreateFromClientToServer();
                    if (sliderService.UpdateImageSlider(imageSliderToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Slider Image Updated",
                            IsUpdated = true
                        };
                    }
                }
                else if (Request.Form["Delete"] != null)
                {
                    try
                    {
                        sliderService.DeleteImageSlider(viewModel.ImageSlider.SliderId);
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Slider Image Deleted",
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
                    viewModel.ImageSlider.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.ImageSlider.RecCreatedDate = DateTime.Now;
                    viewModel.ImageSlider.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.ImageSlider.RecUpdatedDate = DateTime.Now;
                    ImageSlider imageSliderToAdd = viewModel.ImageSlider.CreateFromClientToServer();
                    if (sliderService.AddImageSlider(imageSliderToAdd))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Slider Image Added",
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
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["SliderImage"]);
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