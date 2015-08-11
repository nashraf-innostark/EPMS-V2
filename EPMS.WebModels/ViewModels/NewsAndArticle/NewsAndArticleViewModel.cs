namespace EPMS.WebModels.ViewModels.NewsAndArticle
{
    public class NewsAndArticleViewModel
    {
        public NewsAndArticleViewModel()
        {
            NewsAndArticle = new WebsiteModels.NewsAndArticle();
        }
        public WebsiteModels.NewsAndArticle NewsAndArticle { get; set; }
    }
}