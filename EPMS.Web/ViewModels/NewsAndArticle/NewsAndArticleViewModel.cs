using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.NewsAndArticle
{
    public class NewsAndArticleViewModel
    {
        public NewsAndArticleViewModel()
        {
            NewsAndArticles = new Models.NewsAndArticle();
        }
        public Models.NewsAndArticle NewsAndArticles { get; set; }
    }
}