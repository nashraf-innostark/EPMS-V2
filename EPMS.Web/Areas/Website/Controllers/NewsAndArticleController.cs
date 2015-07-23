using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.NewsAndArticles;
using EPMS.Web.ViewModels.NewsAndArticle;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class NewsAndArticleController : BaseController
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

        #region Index

        public ActionResult Index()
        {
            return View(new NewsAndArticleListViewModel
            {
                NewsAndArticles = newsAndArticleService.GetAll().Select(x=>x.CreateFromServerToClient())
            });
        }

        #endregion

        #endregion
    }
}