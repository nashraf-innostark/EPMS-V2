using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.EnumForDropDown;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.Website.Partner;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.HomePage;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class HomeController : BaseController
    {
        #region Private

        private readonly IImageSliderService sliderService;

        #endregion

        #region Constructor
        public HomeController(IImageSliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        #endregion

        // GET: Website/Home
        public ActionResult Index()
        {
            HomePageResponse response = sliderService.GetHomePageResponse();
            HomePageViewModel viewModel = new HomePageViewModel
            {
                ImageSlider = response.ImageSlider.Select(x => x.CreateFromServerToClient()).ToList(),
                Partners = response.Partners.Select(x=>x.CreateFromServerToClient()).ToList()
            };
            IEnumerable<Position> actionTypes = Enum.GetValues(typeof(Position))
                                                       .Cast<Position>();
            viewModel.Position = from action in actionTypes
                                select new SelectListItem
                                {
                                    Text = action.ToString(),
                                    Value = ((int)action).ToString()
                                };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
    }
}