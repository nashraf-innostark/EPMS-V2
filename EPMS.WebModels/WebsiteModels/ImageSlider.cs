﻿using System;
using System.ComponentModel.DataAnnotations;
using EPMS.WebModels.Resources.Website.Slider;

namespace EPMS.WebModels.WebsiteModels
{
    public class ImageSlider
    {
        public long SliderId { get; set; }
        [Required(ErrorMessageResourceType = typeof (Slider), ErrorMessageResourceName = "TitleValidationError")]
        public string TitleEn { get; set; }
        [Required(ErrorMessageResourceType = typeof (Slider), ErrorMessageResourceName = "TItleArValidationError")]
        public string TitleAr { get; set; }
        public string SubTitleEn { get; set; }
        public string SubTitleAr { get; set; }
        public int? ImageOrder { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int? Position { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
    }
}