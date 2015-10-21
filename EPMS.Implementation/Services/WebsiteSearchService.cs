using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    class WebsiteSearchService : IWebsiteSearchService
    {
        private readonly IProductRepository productRepository;
        private readonly INewsAndArticleRepository newsAndArticleRepository;
        private readonly IWebsiteServicesRepository websiteServicesRepository;
        private readonly IAboutUsRepository aboutUsRepository;
        private readonly IContactUsRepository contactUsRepository;

        public WebsiteSearchService(IProductRepository productRepository, INewsAndArticleRepository newsAndArticleRepository, IWebsiteServicesRepository websiteServicesRepository, IAboutUsRepository aboutUsRepository, IContactUsRepository contactUsRepository)
        {
            this.productRepository = productRepository;
            this.newsAndArticleRepository = newsAndArticleRepository;
            this.websiteServicesRepository = websiteServicesRepository;
            this.aboutUsRepository = aboutUsRepository;
            this.contactUsRepository = contactUsRepository;
        }

        public WebsiteSearchResultData GetWebsiteSearchResultData(string search)
        {
            WebsiteSearchResultData searchResultData=new WebsiteSearchResultData
            {
                Products = productRepository.SearchInProducts(search).ToList(),
                NewsAndArticles = newsAndArticleRepository.SearchInNewsAndArticle(search).ToList(),
                WebsiteServices = websiteServicesRepository.SearchInWebsiteService(search).ToList(),
                AboutUs = aboutUsRepository.SearchAboutUs(search),
                ContactUs = contactUsRepository.SearchContactUs(search)
            };
            return searchResultData;
        }
    }
}
