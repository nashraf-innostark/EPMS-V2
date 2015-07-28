namespace EPMS.Web.ViewModels.NewsAndArticle
{
    public class NewsAndArticleViewModel
    {
        public NewsAndArticleViewModel()
        {
            NewsAndArticle = new Models.NewsAndArticle();
        }
        public Models.NewsAndArticle NewsAndArticle { get; set; }
    }
}