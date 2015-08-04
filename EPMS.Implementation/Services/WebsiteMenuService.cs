using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.MenuModels;

namespace EPMS.Implementation.Services
{
    public sealed class WebsiteMenuService:IWebsiteMenuService
    {
        private readonly IProductSectionRepository productSectionRepository;

        public WebsiteMenuService(IProductSectionRepository productSectionRepository)
        {
            this.productSectionRepository = productSectionRepository;
        }

        public WebsiteMenuModel LoadWebsiteMenuItems()
        {
            WebsiteMenuModel websiteMenuModel=new WebsiteMenuModel();
            websiteMenuModel.ProductSections = productSectionRepository.GetAll().ToList();

            return websiteMenuModel;
        }
    }
}
