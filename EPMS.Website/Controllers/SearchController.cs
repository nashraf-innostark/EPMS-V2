using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.WebModels.ModelMappers.Website.AboutUs;
using EPMS.WebModels.ModelMappers.Website.ContactUs;
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
            SearchResult searchResult = new SearchResult();
            searchResult.NewsAndArticleSearchRequest.iDisplayLength = 5;
            searchResult.ProductSearchRequest.iDisplayLength = 5;
            searchResult.WebsiteServiceSearchRequest.iDisplayLength = 5;

            var searchResultData =
                websiteSearchService.GetWebsiteSearchResultData(searchResult.NewsAndArticleSearchRequest,
                    searchResult.ProductSearchRequest, searchResult.WebsiteServiceSearchRequest, search);

            //Products Mapping
            searchResult.Products =
                searchResultData.ProductResponse.Products.Select(x => x.CreateFromServerToClientFromInventory())
                    .ToList();
            searchResult.ProductSearchRequest.TotalCount = searchResultData.ProductResponse.TotalCount;
            searchResult.ShowProductPrice = searchResultData.ShowProductPrice;

            //News and Articles Mapping
            searchResult.NewsAndArticles =
                searchResultData.NewsAndArticleResponse.NewsAndArticles.Select(x => x.CreateFromServerToClient())
                    .ToList();

            searchResult.NewsAndArticleSearchRequest.TotalCount = searchResultData.NewsAndArticleResponse.TotalCount;

            //Services Mapping
            searchResult.WebsiteServices =
                searchResultData.WebsiteSearchResponse.WebsiteServices.Select(x => x.CreateFromServerToClient())
                    .ToList();
            searchResult.WebsiteServiceSearchRequest.TotalCount = searchResultData.WebsiteSearchResponse.TotalCount;


            ViewBag.SearchText = search;
            return View(searchResult);
        }

        [HttpPost]
        public ActionResult Index(SearchResult searchResult)
        {
            var search = searchResult.searchText;
            searchResult.NewsAndArticleSearchRequest.iDisplayLength = 5;
            searchResult.ProductSearchRequest.iDisplayLength = 5;
            searchResult.WebsiteServiceSearchRequest.iDisplayLength = 5;

            var searchResultData =
                websiteSearchService.GetWebsiteSearchResultData(searchResult.NewsAndArticleSearchRequest,
                    searchResult.ProductSearchRequest, searchResult.WebsiteServiceSearchRequest, search);
            if (searchResultData.AboutUs != null)
                searchResult.AboutUs = searchResultData.AboutUs.CreateFromServerToClient();
            if (searchResultData.ContactUs != null)
            searchResult.ContactUs = searchResultData.ContactUs.CreateFromServerToClient();

            //Products Mapping
            searchResult.Products =
                searchResultData.ProductResponse.Products.Select(x => x.CreateFromServerToClientFromInventory())
                    .ToList();
            searchResult.ProductSearchRequest.TotalCount = searchResultData.ProductResponse.TotalCount;

            //News and Articles Mapping
            searchResult.NewsAndArticles =
                searchResultData.NewsAndArticleResponse.NewsAndArticles.Select(x => x.CreateFromServerToClient())
                    .ToList();
            searchResult.NewsAndArticleSearchRequest.TotalCount = searchResultData.NewsAndArticleResponse.TotalCount;

            //Services Mapping
            searchResult.WebsiteServices =
                searchResultData.WebsiteSearchResponse.WebsiteServices.Select(x => x.CreateFromServerToClient())
                    .ToList();
            searchResult.WebsiteServiceSearchRequest.TotalCount = searchResultData.WebsiteSearchResponse.TotalCount;

            //To set the Search Text through ViewBag
            ViewBag.SearchText = searchResult.searchText;
            return View(searchResult);
        }

    }
}