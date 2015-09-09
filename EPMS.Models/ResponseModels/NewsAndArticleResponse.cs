using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class NewsAndArticleResponse
    {
        public List<NewsAndArticle> NewsAndArticles { get; set; }
        public int TotalCount { get; set; }
    }
}
