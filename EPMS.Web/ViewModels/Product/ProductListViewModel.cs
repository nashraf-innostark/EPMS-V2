using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.Product
{
    public class ProductListViewModel
    {
        public IEnumerable<Models.Product> Products { get; set; }
    }
}