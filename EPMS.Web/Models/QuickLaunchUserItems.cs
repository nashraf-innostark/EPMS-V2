using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.Models
{
    public class QuickLaunchUserItems
    {
        public string UserId { get; set; }
        public int MenuId { get; set; }
        public string ImagePath { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}