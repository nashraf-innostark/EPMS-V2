﻿using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.NewsAndArticle
{
    public class NewsAndArticleListViewModel
    {
        public IEnumerable<WebsiteModels.NewsAndArticle> NewsAndArticles { get; set; }
        public bool NewsOrArticle { get; set; }
    }
}