using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ProductResponse
    {
        public Product Product { get; set; }
        public List<ProductSection> ProductSections { get; set; }

    }
}
