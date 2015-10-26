using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    internal class WebsiteSearchService : IWebsiteSearchService
    {
        private readonly IProductRepository productRepository;
        private readonly INewsAndArticleRepository newsAndArticleRepository;
        private readonly IWebsiteServicesRepository websiteServicesRepository;

        public WebsiteSearchService(IProductRepository productRepository,
            INewsAndArticleRepository newsAndArticleRepository, IWebsiteServicesRepository websiteServicesRepository)
        {
            this.productRepository = productRepository;
            this.newsAndArticleRepository = newsAndArticleRepository;
            this.websiteServicesRepository = websiteServicesRepository;
        }

        public WebsiteSearchResultData GetWebsiteSearchResultData(
            NewsAndArticleSearchRequest newsAndArticleSearchRequest, ProductSearchRequest productSearchRequest, WebsiteServiceSearchRequest websiteServiceSearchRequest,
            string search)
        {
            WebsiteSearchResultData searchResultData = new WebsiteSearchResultData
            {
                ProductResponse = productRepository.SearchInProducts(productSearchRequest, search),
                NewsAndArticleResponse =
                    newsAndArticleRepository.GetNewsAndArticleListForSearch(newsAndArticleSearchRequest, search),
                WebsiteSearchResponse = websiteServicesRepository.SearchInWebsiteService(websiteServiceSearchRequest, search)
            };
            return searchResultData;
        }
    }
}