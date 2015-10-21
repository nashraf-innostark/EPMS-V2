using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.NewsAndArticles;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ModelMappers.Website.Services;
using EPMS.WebModels.ViewModels.Search;

namespace EPMS.Website.Controllers
{
    public class SearchController : BaseController
    {
        private readonly IWebsiteSearchService websiteSearchService;

        public SearchController(IWebsiteSearchService websiteSearchService)
        {
            this.websiteSearchService = websiteSearchService;
        }

        // GET: Search
        public ActionResult Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return View(new SearchResult());
            }
            var searchResultData = websiteSearchService.GetWebsiteSearchResultData(search);
            SearchResult searchResult=new SearchResult
            {
                Products = searchResultData.Products.Select(x => x.CreateFromServerToClientFromInventory()).ToList(),
                NewsAndArticles = searchResultData.NewsAndArticles.Select(x => x.CreateFromServerToClient()).ToList(),
                WebsiteServices = searchResultData.WebsiteServices.Select(x => x.CreateFromServerToClient()).ToList()
            };
            return View(searchResult);
        }
    }
}