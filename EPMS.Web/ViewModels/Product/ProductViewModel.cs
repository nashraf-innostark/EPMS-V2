using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.Product
{
    public class ProductViewModel
    {
        #region Constructor

        public ProductViewModel()
        {
            Product = new Models.Product();
        }

        #endregion
        public Models.Product Product { get; set; }
    }
}