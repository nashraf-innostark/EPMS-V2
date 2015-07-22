using System;

namespace EPMS.Web.Models
{
    public class ImageSlider
    {
        public long SliderId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string SubTitleEn { get; set; }
        public string SubTitleAr { get; set; }
        public int? ImageOrder { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int? Position { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
    }
}