using System;
using System.Web.Mvc;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Slider;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class SliderController : BaseController
    {
        // GET: Website/Slider
        public ActionResult Create()
        {
            SliderViewModel viewModel = new SliderViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SliderViewModel viewModel)
        {
            try
            {
                if (viewModel.ImageSlider.SliderId > 0)
                {
                    viewModel.ImageSlider.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.ImageSlider.RecUpdatedDate = DateTime.Now;
                    ImageSlider imageSliderToUpdate = viewModel.ImageSlider.CreateFromClientToServer();
                }
                else
                {
                    viewModel.ImageSlider.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.ImageSlider.RecCreatedDate = DateTime.Now;
                    viewModel.ImageSlider.RecUpdatedBy = User.Identity.GetUserId();
                    viewModel.ImageSlider.RecUpdatedDate = DateTime.Now;
                    ImageSlider imageSliderToAdd = viewModel.ImageSlider.CreateFromClientToServer();
                }
                return RedirectToAction("Index", "Home", new { Area = "Website" });
            }
            catch (Exception)
            {
                return View(viewModel);
            }
        }
    }
}