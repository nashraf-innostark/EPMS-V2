using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.MenuModels;

namespace EPMS.Implementation.Services
{
    public sealed class WebsiteMenuService:IWebsiteMenuService
    {
        private readonly IProductSectionRepository productSectionRepository;
        private readonly IWebsiteServicesService websiteService;

        public WebsiteMenuService(IProductSectionRepository productSectionRepository, IWebsiteServicesService websiteService)
        {
            this.productSectionRepository = productSectionRepository;
            this.websiteService = websiteService;
        }

        public WebsiteMenuModel LoadWebsiteMenuItems()
        {
            WebsiteMenuModel websiteMenuModel=new WebsiteMenuModel();
            websiteMenuModel.ProductSections = productSectionRepository.GetAll().ToList();
            websiteMenuModel.WebsiteServices = websiteService.GetAll().ToList();
            return websiteMenuModel;
        }
    }
}
