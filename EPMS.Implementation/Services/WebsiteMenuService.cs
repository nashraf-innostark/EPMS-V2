using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.MenuModels;

namespace EPMS.Implementation.Services
{
    public sealed class WebsiteMenuService:IWebsiteMenuService
    {
        private readonly IProductSectionRepository productSectionRepository;
        private readonly IWebsiteServicesRepository servicesRepository;

        public WebsiteMenuService(IProductSectionRepository productSectionRepository, IWebsiteServicesRepository servicesRepository)
        {
            this.productSectionRepository = productSectionRepository;
            this.servicesRepository = servicesRepository;
        }

        public WebsiteMenuModel LoadWebsiteMenuItems()
        {
            WebsiteMenuModel websiteMenuModel=new WebsiteMenuModel();
            websiteMenuModel.ProductSections = productSectionRepository.GetAll().ToList();
            websiteMenuModel.WebsiteServices = servicesRepository.GetAllPublicServices().ToList();
            return websiteMenuModel;
        }
    }
}
