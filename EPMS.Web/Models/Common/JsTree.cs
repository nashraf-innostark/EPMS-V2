using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.Models.Common
{
    public class JsTree
    {
        public long NodeId { get; set; }
        public long ParentId { get; set; }
        public string NodeTitleEn { get; set; }
        public string NodeTitleAr { get; set; }
    }
}