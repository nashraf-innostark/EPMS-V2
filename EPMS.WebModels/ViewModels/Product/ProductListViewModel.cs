using System.Collections.Generic;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.Product
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            NewArrivals = new List<WebsiteModels.Product>();
            BestSell = new List<WebsiteModels.Product>();
            RandomProducts = new List<WebsiteModels.Product>();
            FeaturedProducts = new List<WebsiteModels.Product>();
        }
        public IList<WebsiteModels.Product> Products { get; set; }
        public IList<WebsiteModels.Product> NewArrivals { get; set; }
        public IList<WebsiteModels.Product> BestSell { get; set; }
        public IList<WebsiteModels.Product> RandomProducts { get; set; }
        public IList<WebsiteModels.Product> FeaturedProducts { get; set; }
        public ProductSearchRequest SearchRequest { get; set; }
        public IEnumerable<Models.DomainModels.ProductSection> ProductSections { get; set; }
    }

    public class ProductLvModel
    {
        public ProductLvModel()
        {
            aaData = new List<WebsiteModels.Product>();
        }

        public ProductListViewResponse SearchRequest { get; set; }
        public List<WebsiteModels.Product> aaData { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        public string sEcho;
    }
}