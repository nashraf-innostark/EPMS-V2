using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.ProductSection
{
    public class ProductSectionListViewModel
    {
        public IEnumerable<Models.ProductSection> ProductSections { get; set; } 
    }
}