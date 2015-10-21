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

        public WebsiteSearchService(IProductRepository productRepository, INewsAndArticleRepository newsAndArticleRepository, IWebsiteServicesRepository websiteServicesRepository)
        {
            this.productRepository = productRepository;
            this.newsAndArticleRepository = newsAndArticleRepository;
            this.websiteServicesRepository = websiteServicesRepository;
        }

        public WebsiteSearchResultData GetWebsiteSearchResultData(string search)
        {
            WebsiteSearchResultData searchResultData=new WebsiteSearchResultData
            {
                Products = productRepository.SearchInProducts(search).ToList(),
                NewsAndArticles = newsAndArticleRepository.SearchInNewsAndArticle(search).ToList(),
                WebsiteServices = websiteServicesRepository.SearchInWebsiteService(search).ToList()
            };
            return searchResultData;
        }
    }
}
