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
        private readonly IAboutUsRepository aboutUsRepository;
        private readonly IContactUsRepository contactUsRepository;
        private readonly IWebsiteHomePageRepository homePageRepository;

        public WebsiteSearchService(IProductRepository productRepository,
            INewsAndArticleRepository newsAndArticleRepository, IWebsiteServicesRepository websiteServicesRepository,
            IAboutUsRepository aboutUsRepository, IContactUsRepository contactUsRepository, IWebsiteHomePageRepository homePageRepository)
        {
            this.productRepository = productRepository;
            this.newsAndArticleRepository = newsAndArticleRepository;
            this.websiteServicesRepository = websiteServicesRepository;
            this.aboutUsRepository = aboutUsRepository;
            this.contactUsRepository = contactUsRepository;
            this.homePageRepository = homePageRepository;
        }

        public WebsiteSearchResultData GetWebsiteSearchResultData(
            NewsAndArticleSearchRequest newsAndArticleSearchRequest, ProductSearchRequest productSearchRequest,
            WebsiteServiceSearchRequest websiteServiceSearchRequest, string search)
        {
            WebsiteSearchResultData searchResultData = new WebsiteSearchResultData
            {
                ProductResponse = productRepository.SearchInProducts(productSearchRequest, search),
                NewsAndArticleResponse = newsAndArticleRepository.GetNewsAndArticleListForSearch(newsAndArticleSearchRequest, search),
                WebsiteSearchResponse = websiteServicesRepository.SearchInWebsiteService(websiteServiceSearchRequest, search),
                AboutUs = aboutUsRepository.SearchAboutUs(search),
                ContactUs = contactUsRepository.SearchContactUs(search),
                ShowProductPrice = homePageRepository.GetHomePageResponse().ShowProductPrice
            };
            return searchResultData;
        }
    }
}