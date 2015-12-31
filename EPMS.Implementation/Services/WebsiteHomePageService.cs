using System;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class WebsiteHomePageService : IWebsiteHomePageService
    {
        #region Private

        private readonly IWebsiteDepartmentService websiteDepartmentService;
        private readonly IPartnerService partnerService;
        private readonly IWebsiteHomePageRepository repository;
        private readonly INewsAndArticleService newsAndArticleService;
        private readonly IWebsiteServicesService servicesService;
        private readonly IAboutUsService aboutUsService;

        #endregion

        #region Constructor

        public WebsiteHomePageService(IWebsiteDepartmentService websiteDepartmentService, IPartnerService partnerService, IWebsiteHomePageRepository repository, INewsAndArticleService newsAndArticleService, IWebsiteServicesService servicesService, IAboutUsService aboutUsService)
        {
            this.websiteDepartmentService = websiteDepartmentService;
            this.partnerService = partnerService;
            this.repository = repository;
            this.newsAndArticleService = newsAndArticleService;
            this.servicesService = servicesService;
            this.aboutUsService = aboutUsService;
        }

        #endregion

        #region Public

        public WebsiteHomePageResponse GetHomePageResponse()
        {
            //MetaTagsResponse response = GetMetaTags();
            var homePage = repository.GetHomePageResponse();
            return new WebsiteHomePageResponse
            {
                HomePage = homePage,
                //MetaTagsResponse = response
            };
        }

        public WebsiteHomeResponse WebsiteHomeResponse()
        {
            WebsiteHomeResponse response = new WebsiteHomeResponse
            {
                WebsiteDepartments = websiteDepartmentService.GetAll().Where(x=>x.ShowToPublic),
                Partners = partnerService.GetAll(),
            };
            return response;
        }

        public bool SaveLogo(WebsiteHomePage homePage)
        {
            try
            {
                var dbLogo = repository.GetHomePageResponse();
                if (dbLogo != null)
                {
                    dbLogo.WebsiteLogoPath = homePage.WebsiteLogoPath;
                    repository.Update(dbLogo);
                    repository.SaveChanges();
                }
                else
                {
                    repository.Add(homePage);
                    repository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateShowProductPrice(bool showPrice)
        {
            try
            {
                var dbData = repository.GetHomePageResponse();
                if (dbData != null)
                {
                    dbData.ShowProductPrice = showPrice;
                    repository.Update(dbData);
                    repository.SaveChanges();
                }
                else
                {
                    WebsiteHomePage homePage = new WebsiteHomePage
                    {
                        WebsiteLogoPath = "",
                        ShowProductPrice = showPrice
                    };
                    repository.Add(homePage);
                    repository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddWebsiteLogo(WebsiteHomePage homePage)
        {
            try
            {
                repository.Add(homePage);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateWebsiteLogo(WebsiteHomePage homePage)
        {
            try
            {
                repository.Update(homePage);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public MetaTagsResponse GetMetaTags()
        {
            MetaTagsResponse response = new MetaTagsResponse();
            var newsArticles = newsAndArticleService.GetAll();
            var services = servicesService.GetAll();
            var aboutUs = aboutUsService.GetDetail();
            foreach (var newsArticle in newsArticles)
            {
                if (!string.IsNullOrEmpty(newsArticle.MetaKeywords)) response.Name += newsArticle.MetaKeywords + ",";
                if (!string.IsNullOrEmpty(newsArticle.MetaKeywordsAr)) response.Name += newsArticle.MetaKeywordsAr + ",";
                if (!string.IsNullOrEmpty(newsArticle.MetaDesc)) response.Description += newsArticle.MetaDesc + ",";
                if (!string.IsNullOrEmpty(newsArticle.MetaDescAr)) response.Description += newsArticle.MetaDescAr + ",";
            }
            foreach (var service in services)
            {
                if (!string.IsNullOrEmpty(service.MetaKeywordsEn)) response.Name += service.MetaKeywordsEn + ",";
                if (!string.IsNullOrEmpty(service.MetaKeywordsAr)) response.Name += service.MetaKeywordsAr + ",";
                if (!string.IsNullOrEmpty(service.MetaDescriptionEn)) response.Description += service.MetaDescriptionEn + ",";
                if (!string.IsNullOrEmpty(service.MetaDescriptionAr)) response.Description += service.MetaDescriptionAr + ",";
            }
            if (!string.IsNullOrEmpty(aboutUs.MetaKeywords))  response.Name += aboutUs.MetaKeywords + ",";
            if (!string.IsNullOrEmpty(aboutUs.MetaKeywordsAr)) response.Name += aboutUs.MetaKeywordsAr + ",";
            if (!string.IsNullOrEmpty(aboutUs.MetaDesc)) response.Description += aboutUs.MetaDesc + ",";
            if (!string.IsNullOrEmpty(aboutUs.MetaDescAr)) response.Description += aboutUs.MetaDescAr + ",";
            return response;
        }

        #endregion

    }
}
