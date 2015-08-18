using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.NewsAndArticles;
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
            if (id == 1)
            {
                return View(new NewsAndArticleListViewModel
                {
                    NewsAndArticles =
                        newsAndArticleService.GetAll()
                            .Select(x => x.CreateFromServerToClient())
                            .Where(y=>y.Type).OrderBy(y => y.SortOrder)
                });
            }
            return View(new NewsAndArticleListViewModel
            {
                NewsAndArticles =
                    newsAndArticleService.GetAll()
                        .Select(x => x.CreateFromServerToClient())
                        .Where(y => y.Type == false).OrderBy(y => y.SortOrder)
            });
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