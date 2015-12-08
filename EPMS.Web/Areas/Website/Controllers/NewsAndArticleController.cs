using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers.Website.NewsAndArticles;
using EPMS.WebModels.ViewModels.NewsAndArticle;
using EPMS.Web.Controllers;
using EPMS.WebModels.ViewModels.Common;
using Microsoft.AspNet.Identity;

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

        #region News Index

        [SiteAuthorize(PermissionKey = "NewsIndex")]
        public ActionResult Index()
        {
            return View(new NewsAndArticleListViewModel
            {
                NewsAndArticles = newsAndArticleService.GetAll().Where(x => x.Type == false).Select(x => x.CreateFromServerToClient()).OrderBy(y => y.SortOrder)
            });
        }

        #endregion

        #region News Create

        [SiteAuthorize(PermissionKey = "NewsCreate")]
        public ActionResult Create(long? id)
        {
            NewsAndArticleViewModel newsAndArticleViewModel = new NewsAndArticleViewModel();
            if (id != null)
            {
                newsAndArticleViewModel.NewsAndArticle =
                    newsAndArticleService.FindNewsAndArticleById((long)id).CreateFromServerToClient();
            }
            return View(newsAndArticleViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsAndArticleViewModel newsAndArticleViewModel)
        {
            try
            {
                if (newsAndArticleViewModel.NewsAndArticle.NewsArticleId > 0)
                {
                    //Update Case
                    newsAndArticleViewModel.NewsAndArticle.RecLastUpdatedDt = DateTime.Now;
                    newsAndArticleViewModel.NewsAndArticle.RecLastUpdatedBy = User.Identity.GetUserId();
                    NewsAndArticle newsAndArticleToUpdate =
                        newsAndArticleViewModel.NewsAndArticle.CreateFromClientToServer();
                    if (newsAndArticleService.UpdateNewsAndArticle(newsAndArticleToUpdate))
                    {
                        TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.Website.NewsAndArticles.NewsAndArticlesList.Updated, IsUpdated = true };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //Add Case
                    newsAndArticleViewModel.NewsAndArticle.RecCreatedDt = DateTime.Now;
                    newsAndArticleViewModel.NewsAndArticle.RecCreatedBy = User.Identity.GetUserId();
                    newsAndArticleViewModel.NewsAndArticle.RecLastUpdatedDt = DateTime.Now;
                    newsAndArticleViewModel.NewsAndArticle.RecLastUpdatedBy = User.Identity.GetUserId();
                    NewsAndArticle newsAndArticleToAdd =
                        newsAndArticleViewModel.NewsAndArticle.CreateFromClientToServer();
                    if (newsAndArticleService.AddNewsAndArticle(newsAndArticleToAdd))
                    {
                        TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.Website.NewsAndArticles.NewsAndArticlesList.Added, IsSaved = true };
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = e.Message, IsError = true };
                return RedirectToAction("Create", e);
            }
            return View(newsAndArticleViewModel);
        }

        #endregion

        #region Article Index

        [SiteAuthorize(PermissionKey = "ArticlesIndex")]
        public ActionResult Articles()
        {
            return View(new NewsAndArticleListViewModel
            {
                NewsAndArticles = newsAndArticleService.GetAll().Where(x => x.Type).Select(x => x.CreateFromServerToClient()).OrderBy(y => y.SortOrder)
            });
        }

        #endregion

        #region Article Create

        [SiteAuthorize(PermissionKey = "ArticleCreate")]
        public ActionResult ArticleCreate(long? id)
        {
            NewsAndArticleViewModel newsAndArticleViewModel = new NewsAndArticleViewModel();
            if (id != null)
            {
                newsAndArticleViewModel.NewsAndArticle =
                    newsAndArticleService.FindNewsAndArticleById((long)id).CreateFromServerToClient();
            }
            return View(newsAndArticleViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ArticleCreate(NewsAndArticleViewModel newsAndArticleViewModel)
        {
            try
            {
                if (newsAndArticleViewModel.NewsAndArticle.NewsArticleId > 0)
                {
                    //Update Case
                    newsAndArticleViewModel.NewsAndArticle.RecLastUpdatedDt = DateTime.Now;
                    newsAndArticleViewModel.NewsAndArticle.RecLastUpdatedBy = User.Identity.GetUserId();
                    NewsAndArticle newsAndArticleToUpdate =
                        newsAndArticleViewModel.NewsAndArticle.CreateFromClientToServer();
                    if (newsAndArticleService.UpdateNewsAndArticle(newsAndArticleToUpdate))
                    {
                        TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.Website.NewsAndArticles.NewsAndArticlesList.Updated, IsUpdated = true };
                        return RedirectToAction("Articles");
                    }
                }
                else
                {
                    //Add Case
                    newsAndArticleViewModel.NewsAndArticle.RecCreatedDt = DateTime.Now;
                    newsAndArticleViewModel.NewsAndArticle.RecCreatedBy = User.Identity.GetUserId();
                    newsAndArticleViewModel.NewsAndArticle.RecLastUpdatedDt = DateTime.Now;
                    newsAndArticleViewModel.NewsAndArticle.RecLastUpdatedBy = User.Identity.GetUserId();
                    NewsAndArticle newsAndArticleToAdd =
                        newsAndArticleViewModel.NewsAndArticle.CreateFromClientToServer();
                    if (newsAndArticleService.AddNewsAndArticle(newsAndArticleToAdd))
                    {
                        TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.Website.NewsAndArticles.NewsAndArticlesList.Added, IsSaved = true };
                        return RedirectToAction("Articles");
                    }
                }
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = e.Message, IsError = true };
                return RedirectToAction("ArticleCreate", e);
            }
            return View(newsAndArticleViewModel);
        }

        #endregion

        #region Upload Image

        public ActionResult UploadImage()
        {
            HttpPostedFileBase image = Request.Files[0];
            var filename = "";
            try
            {
                //Save File to Folder
                if ((image != null))
                {
                    filename =
                        (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + image.FileName)
                            .Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["NewsOrArticleImage"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    image.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return
                    Json(
                        new
                        {
                            response = "Failed to upload. Error: " + exp.Message,
                            status = (int)HttpStatusCode.BadRequest
                        }, JsonRequestBehavior.AllowGet);
            }
            return
                Json(
                    new
                    {
                        filename = filename,
                        size = image.ContentLength / 1024 + "KB",
                        response = "Successfully uploaded!",
                        status = (int)HttpStatusCode.OK
                    }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Json

        public JsonResult DeleteIt(long abcd)
        {
            newsAndArticleService.Delete(abcd);
            return Json("Deleted", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}