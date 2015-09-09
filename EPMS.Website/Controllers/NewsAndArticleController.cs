using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.NewsAndArticles;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.NewsAndArticle;

namespace EPMS.Website.Controllers
{
    public class NewsAndArticleController : Controller
    {
        #region Private

        private readonly INewsAndArticleService newsAndArticleService;

        #endregion

        #region Constructor

        public NewsAndArticleController(INewsAndArticleService newsAndArticleService)
        {
            this.newsAndArticleService = newsAndArticleService;
        }

        #endregion

        #region Public

        #region List of News/Articles

        public ActionResult Index(long id)
        {
            ViewBag.ShowSlider = false;
            NewsAndArticleListViewModel viewModel = new NewsAndArticleListViewModel();
            viewModel.SearchRequest = new NewsAndArticleSearchRequest
            {
                iDisplayLength = 5,
            };

            var response = newsAndArticleService.GetNewsAndArticleList(viewModel.SearchRequest, id == 1);
            viewModel.NewsAndArticles = response.NewsAndArticles.Select(x => x.CreateFromServerToClient());
            viewModel.SearchRequest.TotalCount = response.TotalCount;
            viewModel.Type = id;
            return View(viewModel);
        }

        [HttpPost]

        public ActionResult Index(NewsAndArticleListViewModel viewModel)
        {
            viewModel.SearchRequest.iDisplayLength = 5;
            NewsAndArticleResponse productsList = newsAndArticleService.GetNewsAndArticleList(viewModel.SearchRequest, viewModel.Type == 1);
            if (productsList.NewsAndArticles.Any())
            {
                viewModel.NewsAndArticles = productsList.NewsAndArticles.Select(x => x.CreateFromServerToClient()).ToList();
                viewModel.SearchRequest.TotalCount = productsList.TotalCount;
            }
            ViewBag.ShowSlider = false;
            ViewBag.From = viewModel.SearchRequest.From;
            return View(viewModel);
        }

        #endregion

        #region Detail Page

        public ActionResult Detail(long id)
        {
            ViewBag.ShowSlider = false;
            NewsAndArticleViewModel newsAndArticleViewModel = new NewsAndArticleViewModel
            {
                NewsAndArticle = newsAndArticleService.FindNewsAndArticleById(id).CreateFromServerToClient()
            };
            return View(newsAndArticleViewModel);
        }

        #endregion

        #endregion
    }
}